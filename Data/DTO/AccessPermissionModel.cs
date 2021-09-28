using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    public class AccessPermissionModel
    {
        public AccessPermissionModel()
        {
            PagePermission = new List<PagePermission>();
            ActionPermissionModel = new List<ActionPermissionModel>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public bool IsActive { get; set; }
        public List<PagePermission> PagePermission { get; set; }
        public List<ActionPermissionModel> ActionPermissionModel { get; set; }
    }

    public class PagePermission
    {
        public int ID { get; set; }
        public string User { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class ActionPermissionModel
    {
        public int ID { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}
