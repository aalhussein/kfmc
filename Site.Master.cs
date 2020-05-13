using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace kfmc
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            showHideMenuBasedOnRole();
        }

        private void showHideMenuBasedOnRole()
        {
            // this code hide admin menu from all other users
            if (!Roles.IsUserInRole("admin"))
            {
                MenuItemCollection menuItems = NavigationMenu.Items;
                MenuItem adminItem = new MenuItem();
                foreach (MenuItem menuItem in menuItems)
                {
                    if (menuItem.Text == "Admin"  || (menuItem.Text == "MaxConPoolReport"))// to hide specific menu put  the name here ... Warning, case sensitive 
                        adminItem = menuItem;
                }
                menuItems.Remove(adminItem);
            }
            // hide menu if user is not in finance 
            if (!Roles.IsUserInRole("finance"))
            {
                MenuItemCollection SiteMenuItems = NavigationMenu.Items;
                MenuItem myMenuName = new MenuItem();
                foreach (MenuItem menuItem in SiteMenuItems)
                {
                    if (menuItem.Text == "Finance")// to hide specific menu put  the name here ... Warning, case sensitive 
                        myMenuName = menuItem;
                }
                SiteMenuItems.Remove(myMenuName);
            }
            // hide menu if user is not in  marketing 
            if (!Roles.IsUserInRole("marketing"))
            {
                MenuItemCollection SiteMenuItems = NavigationMenu.Items;
                MenuItem myMenuName = new MenuItem();
                foreach (MenuItem menuItem in SiteMenuItems)
                {
                    if (menuItem.Text == "Marketing")// to hide specific menu put  the name here ... Warning, case sensitive 
                        myMenuName = menuItem;
                }
                SiteMenuItems.Remove(myMenuName);
            }
            if (!Roles.IsUserInRole("manager"))
            {
                MenuItemCollection SiteMenuItems = NavigationMenu.Items;
                MenuItem myMenuName = new MenuItem();
                foreach (MenuItem menuItem in SiteMenuItems)
                {
                    if (menuItem.Text == "Manager")// to hide specific menu put  the name here ... Warning, case sensitive 
                        myMenuName = menuItem;
                }
                SiteMenuItems.Remove(myMenuName);
            }

            if (!Roles.IsUserInRole("employee"))
            {
                MenuItemCollection SiteMenuItems = NavigationMenu.Items;
                MenuItem myMenuName = new MenuItem();
                foreach (MenuItem menuItem in SiteMenuItems)
                {
                    if (menuItem.Text == "Employee")// to hide specific menu put  the name here ... Warning, case sensitive 
                        myMenuName = menuItem;
                }
                SiteMenuItems.Remove(myMenuName);
            }
        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {

        }

    }
}