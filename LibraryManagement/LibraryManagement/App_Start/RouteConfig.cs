using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibraryManagement
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "IssuedBookDetails",
               url: "books/issueddetails",
               defaults: new { controller = "Books", action = "IssuedDetails" }
           );

            routes.MapRoute(
             name: "NewProposalsDetails",
             url: "books/newproposalsdetails",
             defaults: new { controller = "Books", action = "NewProposalsDetails" }
         );

            routes.MapRoute(
             name: "LostBookDetails",
             url: "books/lostdetails",
             defaults: new { controller = "Books", action = "LostDetails" }
         );

            routes.MapRoute(
          name: "RequestBookDetails",
          url: "books/requestdetails",
          defaults: new { controller = "Books", action = "RequestDetails" }
      );

            routes.MapRoute(
         name: "AddBook",
         url: "books/update",
         defaults: new { controller = "Books", action = "Update" }
     );

            routes.MapRoute(
             name: "GetBookDetails",
             url: "books/getbookdetails",
             defaults: new { controller = "Books", action = "GetBookDetails" }
         );

           routes.MapRoute(
              name: "UserCategoryList",
              url: "books/categorydetails",
              defaults: new { controller = "Books", action = "CategoryDetails" }
           );
            routes.MapRoute(
              name: "SendBookRequest",
              url: "books/sendbookrequest",
              defaults: new { controller = "Books", action = "SendBookRequest" }
            );

            routes.MapRoute(
                name: "UserBookListCollection",
                url: "profile/userbooklistcollection",
                defaults: new { controller = "Profile", action = "UserBookListCollection" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Authentication", action = "Login", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "LoginOut",
                url: "logout",
                defaults: new { controller = "Authentication", action = "LogOut", id = UrlParameter.Optional }
              );

            routes.MapRoute(
             name: "Profile",
             url: "profile",
             defaults: new { controller = "Profile", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
