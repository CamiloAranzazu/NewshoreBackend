
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace newshore.Controllers
{

    public class FilesController : ApiController
    {


        [HttpPost]
        [Route("api/UploadFile")]
        public HttpResponseMessage UploadFile()
        {
            string fileName = null;
            var httpRequest = HttpContext.Current.Request;
            // Upload Image
            var postedFile = httpRequest.Files["Image"];

            // Save Image
            fileName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            fileName = fileName + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + fileName);
            postedFile.SaveAs(filePath);

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpPost]
        [Route("api/UploadResult")]
        public HttpResponseMessage UploadResult([FromBody] string[] resultados)
        {
            string rutaRes = "C:\\Users\\user\\source\\repos\\NewshoreBack\\NewshoreBack\\Image\\RESULTADOS.txt";
            // Save Image
            using (StreamWriter outPutFile = new StreamWriter(rutaRes))
            {
                foreach (var item in resultados)
                {
                    outPutFile.WriteLine(item);
                }
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpGet]
        [Route("api/GetDataFile")]
        public HttpResponseMessage GetDataFile()
        {
            ArrayList con = new ArrayList();
            ArrayList reg = new ArrayList();
            Object[] objs = new Object[] { con, reg };
            string rutaCon = "C:\\Users\\user\\source\\repos\\NewshoreBack\\NewshoreBack\\Image\\CONTENIDO.txt";
            string rutaReg = "C:\\Users\\user\\source\\repos\\NewshoreBack\\NewshoreBack\\Image\\REGISTRADO.txt";

            if (!File.Exists(rutaCon))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "0");
            }
            else if (!File.Exists(rutaReg))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "1");
            }
            else if (File.Exists(rutaReg) && File.Exists(rutaCon))
            {
                using (StreamReader CONTENIDO = new StreamReader(@rutaCon),
                   REGISTRADOS = new StreamReader(@rutaReg))
                {

                    while (!CONTENIDO.EndOfStream)
                    {
                        string contenidos = CONTENIDO.ReadLine();
                        con.Add(contenidos);
                    }
                    while (!REGISTRADOS.EndOfStream)
                    {
                        string registrados = REGISTRADOS.ReadLine();
                        reg.Add(registrados);
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, objs);
        }

    }
}
