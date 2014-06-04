using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class Shelf
    {
        /// <summary>
        /// 货架名称
        /// </summary>
        public int shelf_id { get; set; }
        /// <summary>
        /// 货架信息
        /// </summary>
        public Shelf_Data shelf_data { get; set; }
        /// <summary>
        /// 货架招牌图片URL(图片需要调用图片上传接口获得图片URL填写至此，
        /// 否则会显示出错,建议尺寸640*120，近控件1-4有banner，控件5没有banner)
        /// </summary>
        public string shelf_banner { get; set; }
        /// <summary>
        /// 货架名称
        /// </summary>
        public string shelf_name { get; set; }


        public class Shelf_Data
        {
            /// <summary>
            /// (1,2,3,4)
            /// 
            /// </summary>
            public List<Shelf_Module> module_infos { get; set; }

            public void AddModule1(Shelf_Module1 m1)
            {
                module_infos.Add(m1);
            }
            public void AddModule2(Shelf_Module2 m2)
            {
                module_infos.Add(m2);
            }
            public void AddModule3(Shelf_Module3 m3)
            {
                module_infos.Add(m3);
            }
            public void AddModule4(Shelf_Module4 m4)
            {
                module_infos.Add(m4);
            }
            public void AddModule5(Shelf_Module5 m5)
            {
                module_infos.Add(m5);
            }

            public abstract class Shelf_Module
            {
                public abstract int eid { get; }
            }

            /// <summary>
            /// 控件1是有一个分组组成，展示该分组指定数量的商品列表，可与控件2、3、4联合使用
            /// </summary>
            public class Shelf_Module1 : Shelf_Module
            {
                /// <summary>
                /// 分组信息
                /// </summary>
                public Group_Info group_info { get; set; }

                public override int eid
                {
                    get { return 1; }
                }

                public class Group_Info
                {
                    /// <summary>
                    /// 分组id
                    /// </summary>
                    public int group_id { get; set; }
                    public Filter filter { get; set; }

                    public class Filter
                    {
                        /// <summary>
                        /// 该控件展示商品个数
                        /// </summary>
                        public int count { get; set; }
                    }
                }
            }

            /// <summary>
            /// 控件2是由多个分组组成（最多有4个分组），展示制定分组的名称，可与控件1、3、4联用
            /// </summary>
            public class Shelf_Module2 : Shelf_Module
            {
                public Group_Infos group_infos { get; set; }


                public override int eid
                {
                    get { return 2; }
                }

                public class Group_Infos
                {
                    public List<Group> groups { get; set; }

                    public class Group
                    {
                        public int group_id { get; set; }
                    }
                }
            }

            /// <summary>
            /// 控件3是由一个分组组成，展示指定分组的分组图片，可与控件1、2、4联合使用
            /// </summary>
            public class Shelf_Module3 : Shelf_Module
            {
                public override int eid
                {
                    get { return 3; }
                }

                public Group_Info group_info { get; set; }

                public class Group_Info
                {
                    public int group_id { get; set; }
                    /// <summary>
                    /// 分组照片（建议分辨率600*208）
                    /// </summary>
                    public string img { get; set; }
                }
            }
            /// <summary>
            /// 控件4是由多个分组组成（最多3个分组），展示指定分组的分组图片，可与控件1、2、4联合使用
            /// </summary>
            public class Shelf_Module4 : Shelf_Module
            {
                public override int eid
                {
                    get { return 4; }
                }

                public Group_Infos group_infos { get; set; }

                public class Group_Infos
                {
                    public List<Group> groups { get; set; }

                    public class Group
                    {
                        public int group_id { get; set; }
                        /// <summary>
                        /// 分组照片（3个分组建议分辨率分别为：350*350,244*172,244*,172）
                        /// </summary>
                        public string img { get; set; }
                    }
                }
            }

            /// <summary>
            /// 控件5是由多个分组组成，展示指定分组的名称，不可与其他控件联合使用
            /// </summary>
            public class Shelf_Module5 : Shelf_Module
            {
                public override int eid
                {
                    get { return 5; }
                }
                public Group_Infos group_infos { get; set; }

                public class Group_Infos
                {
                    public List<Group> groups { get; set; }
                    /// <summary>
                    /// 分组照片（建议分辨率640*1008）
                    /// </summary>
                    public string img_background { get; set; }

                    public class Group
                    {
                        public int group_id { get; set; }
                    }
                }

            }
        }
    }

}
