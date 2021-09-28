using Data.Models;
using Microsoft.AspNetCore.Http;
using Service.IServices;
using Service.Services;
using Service.UnitOfServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HISSystem.Helper
{
    public static class PermissionHelper
    {
        private static readonly IUnitOfService _service = new UnitOfService();
        public static List<AccessPermission> _viewAccess = new List<AccessPermission>();
        public static List<ActionPermission> _actionAccess = new List<ActionPermission>();
        public static void GetPermission()
        {
            _viewAccess = _service.AccessPermissionService.GetAll();
            var action = _service.ActionPermissionService.GetAll();
            if(action != null)
            {
                _actionAccess = action;
            }
        }

        public static bool ViewPermission(Page page, int roleID)
        {
            GetPermission();
            var result = _viewAccess.Any(x => x.ViewControl.Name.ToLower() == Convert.ToString(page).ToLower() && x.RoleID == roleID && x.IsEnabled == true);
            return result;
        }

        public static bool ActionPermission(Page viewID, ActionButton button, int roleID)
        {
            GetPermission();
            return _actionAccess.Any(x => x.ActionControl.Name == Convert.ToString(button) && x.RoleID == roleID && x.ViewControlID == Convert.ToInt32(viewID));
        }
    }
}
