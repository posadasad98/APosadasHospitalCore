using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace BL
{
    public class Hospital
    {
        public static Dictionary<string, object> GetAll()
        {
            ML.Hospital hosp = new ML.Hospital();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Hospital", hosp }, { "Exepcion", excepcion }, { "Resultado", false } };

            try
            {
                using (DL.AposadasHospitalContext context = new DL.AposadasHospitalContext())

                {
                    var registros = (from Hospital in context.Hospitals
                     select new
                     {
                         IdHospital = Hospital.IdHospital,
                         Nombre = Hospital.Nombre,
                         Direccion = Hospital.Direccion,
                         AnioDeConstruccion = Hospital.AniodeConstruccion,
                         Capacidad = Hospital.Capacidad,
                         IdEspecialidad = Hospital.IdEspecialidad

                     }).ToList();

                    if (registros != null)
                    {
                        hosp.Hospitales = new List<object>();
                        foreach (var registro in registros)
                        {
                            
                            ML.Hospital hospital = new ML.Hospital();

                            hospital.IdHospital = registro.IdHospital;
                            hospital.Nombre = registro.Nombre;
                            hospital.Direccion = registro.Direccion;
                            hospital.AnioDeConstruccion = (DateTime)registro.AnioDeConstruccion;
                            hospital.Capacidad = (int)registro.Capacidad;


                            hospital.Especialidad = new ML.Especialidad();
                            hospital.Especialidad.IdEspecialidad = (int)registro.IdEspecialidad;



                            hosp.Hospitales.Add(hospital)
;
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = hosp;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }

        public static Dictionary<string, object> Delete(int IdHospital)
        {
            string exepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Hospital", IdHospital }, { "Exepcion", exepcion }, { "Resultado", false } };

            try
            {

                using (DL.AposadasHospitalContext context = new DL.AposadasHospitalContext())
                {

                    var filasAfectadas = context.Database.ExecuteSqlRaw($"HospitalDelete'{IdHospital}'");

                    if (filasAfectadas > 0)

                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }

            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Exepcion"] = ex.Message;
            }
            return diccionario;
        }
    }
    
}
