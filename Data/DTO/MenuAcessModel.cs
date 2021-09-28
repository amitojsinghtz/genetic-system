using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;

namespace Data.DTO
{
    public class MenuAcessModel:AccessPermissionModel
    {
        public MenuAcessModel()
        {
            AllMenu = new List<ViewControl>();
            AllRoles = new List<Role>();
            AllMenuAccess = new List<MenuAcessModel>();
            AllActionPage = new List<ActionControl>();
            AllActionPermission = new List<AccessPermission>();
        }
       
    
        public string ID { get; set; }
        public int ViewControlID { get; set; }
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ViewControl ViewControl { get; set; }
        public virtual Role Role { get; set; }
        public string Name { get; set; }
        public IEnumerable<ViewControl> AllMenu = new List<ViewControl>();
        public IEnumerable<MenuAcessModel> AllMenuAccess = new List<MenuAcessModel>();
        public IEnumerable<Role> AllRoles = new List<Role>();
        public IEnumerable<ActionControl> AllActionPage = new List<ActionControl>();
        public IEnumerable<AccessPermission> AllActionPermission = new List<AccessPermission>();
    }
}
