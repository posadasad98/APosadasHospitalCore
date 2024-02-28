using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class HospitalController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Hospital hospital = new ML.Hospital();
            Dictionary<string, object> result = BL.Hospital.GetAll();
            hospital = (ML.Hospital)result["Hospital"];
            return View(hospital);
        }
        [HttpGet]
        public ActionResult Delete(int IdHospital)
        {
            Dictionary<string, object> result = BL.Hospital.Delete(IdHospital);
            bool resultado = (bool)result["Resultado"];

            if (resultado == true)
            {
                ViewBag.Mensaje = "EL Hospital HA SIDO ELIMINADO";
                return PartialView("Modal");
            }
            else
            {
                
                ViewBag.Mensaje = "ERROR! EL HOSPITAL NO SE HA  PODIDO ELIMINAR "; 
                return PartialView("Modal");
            }
        }
    }
}
