using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;

namespace Loogn.WeiXinSDK
{
    class Tuple<T1, T2, T3>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public Tuple(T1 t1, T2 t2, T3 t3)
        {
            this.Item1 = t1;
            this.Item2 = t2;
            this.Item3 = t3;
        }
    }


    class Util
    {
        #region Http
        public static Stream HttpPost(string action, byte[] data)
        {
            HttpWebRequest myRequest;
            myRequest = WebRequest.Create(action) as HttpWebRequest;
            myRequest.Method = "POST";
            myRequest.Timeout = 20 * 1000;
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            using (Stream newStream = myRequest.GetRequestStream())
            {
                newStream.Write(data, 0, data.Length);
            }
            HttpWebResponse myResponse = myRequest.GetResponse() as HttpWebResponse;
            return myResponse.GetResponseStream();
        }
        public static string HttpPost2(string action, string data)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            using (var stream = Util.HttpPost(action, buffer))
            {
                StreamReader sr = new StreamReader(stream);
                data = sr.ReadToEnd();
                return data;
            }
        }

        public static string HttpUpload(string action, string file)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest myRequest = WebRequest.Create(action) as HttpWebRequest;
            myRequest.Method = "POST";
            myRequest.ContentType = "multipart/form-data;boundary=" + boundary;
            StringBuilder sb = new StringBuilder();
            sb.Append("--" + boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"media\"; filename=\"" + file + "\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: application/octet-stream");
            sb.Append("\r\n\r\n");
            string head = sb.ToString();
            long length = 0;
            byte[] form_data = Encoding.UTF8.GetBytes(head);
            byte[] foot_data = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            length = form_data.Length + foot_data.Length;

            using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                length += fileStream.Length;
                myRequest.ContentLength = length;
                Stream requestStream = myRequest.GetRequestStream();
                requestStream.Write(form_data, 0, form_data.Length);

                byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    requestStream.Write(buffer, 0, bytesRead);
                requestStream.Write(foot_data, 0, foot_data.Length);
            }
            HttpWebResponse myResponse = myRequest.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string json = sr.ReadToEnd().Trim();
            sr.Close();
            if (myResponse != null)
            {
                myResponse.Close();
                myRequest = null;
            }
            if (myRequest != null)
            {
                myRequest = null;
            }
            return json;
        }

        public static Tuple<Stream, string, string> HttpGet(string action)
        {
            HttpWebRequest myRequest = WebRequest.Create(action) as HttpWebRequest;
            myRequest.Method = "GET";
            myRequest.Timeout = 20 * 1000;
            HttpWebResponse myResponse = myRequest.GetResponse() as HttpWebResponse;
            var stream = myResponse.GetResponseStream();
            var ct = myResponse.ContentType;
            if (ct.IndexOf("json") >=0 || ct.IndexOf("text")>=0)
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    var json = sr.ReadToEnd();
                    return new Tuple<Stream, string, string>(null, ct, json);
                }
            }
            else
            {
                Stream MyStream = new MemoryStream();
                byte[] buffer = new Byte[4096];
                int bytesRead = 0;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                    MyStream.Write(buffer, 0, bytesRead);
                MyStream.Position = 0;
                return new Tuple<Stream, string, string>(MyStream, ct, string.Empty);
            }
        }

        public static string HttpGet2(string action)
        {
            return Util.HttpGet(action).Item3;
        }
        #endregion

        #region json
        static JavaScriptSerializer GetJSS()
        {
            return new JavaScriptSerializer();
        }
        public static string ToJson(object obj)
        {
            var jss = GetJSS();
            return jss.Serialize(obj);
        }
        public static void ToJson(object obj, StringBuilder output)
        {
            var jss = GetJSS();
            jss.Serialize(obj, output);
        }

        public static T JsonTo<T>(string json)
        {
            var jss = GetJSS();
            T obj = jss.Deserialize<T>(json);
            return obj;
        }
        public static object JsonTo(string json, Type targetType)
        {
            var jss = GetJSS();
            var obj = jss.DeserializeObject(json);
            return obj;
        }

        public static Dictionary<string, string> GetDictFromXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                dict.Add(node.Name, node.InnerText.Trim());
            }
            return dict;
        }


        #endregion
    }
}
