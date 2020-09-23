using System.Web.Mvc;

namespace EducationManagementSystem.Areas.TrainingStaff
{
    public class TrainingStaffAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TrainingStaff";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TrainingStaff_default",
                "TrainingStaff/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}