using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK
{
    /// <summary>
    /// 微信小店
    /// </summary>
    public class WeiXinShop
    {

        #region 管理管理接口

        public static void AddProduct() { }

        public static void DeleteProduct() { }

        public static void UpdateProduct() { }

        public static void GetProduct() { }

        public static void GetProductListByStatus() { }

        public static void SetProductStatus() { }

        public static void GetSubCateList() { }

        public static void GetSKUListBySubCate() { }

        public static void GetPropertyListByCate() { }

        #endregion

        #region 库存管理接口

        public static void IncInventory() { }

        public static void DecInventory() { }

        #endregion

        #region 邮费模板管理接口

        public static void AddDeliveryTemplate() { }

        public static void DeleteDeliveryTemplate() { }

        public static void UpdateDeliveryTemplate() { }

        public static void GetDeliveryTemplate() { }

        public static void GetDeliveryTemplateList() { }

        #endregion

        #region 分组管理接口
        public static void AddGroup() { }

        public static void DeleteGroup() { }

        public static void UpdateGroupProperty() { }

        public static void UpdateProductGroup() { }

        public static void GetGroupList() { }

        public static void GetGroup() { }

        #endregion

        #region 货架管理接口

        public static void AddShelf() { }

        public static void DeleteShelf() { }

        public static void UpdateShelf() { }

        public static void GetShelfList() { }

        public static void GetShelf() { }

        public static void SetShelfStatus() { }

        #endregion

        #region 订单管理接口

        public static void GetOrder() { } //根据id

        public static void GetOrder(int a) { } //根据订单状态、创建时间

        public static void SetOrderDelivery() { }

        public static void CloseOrder() { }

        #endregion

        #region 功能接口
        public static void UploadImg() { }
        #endregion

    }
}
