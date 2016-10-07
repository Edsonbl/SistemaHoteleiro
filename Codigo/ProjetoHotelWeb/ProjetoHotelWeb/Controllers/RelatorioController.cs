using Dominio.Classes;
using Dominio.Interfaces;
using Dominio.Classes_Especiais;
using Infraestrutura.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.IO;
namespace ProjectHotelWeb.Controllers
{
    public class RelatorioController : Controller
    {
        public IProjectHotel IProjectHotel { get; set; }
        public IPacoteHospedagens IPacoteHospedagem { get; set; }
        public ITipoQuartos ITipoQuarto { get; set; }
        public IPessoas IPessoas { get; set; }
        public IControleClientes IControleCliente { get; set; }
        public IHospedagens IHospedagem { get; set; }
        public IQuartos IQuarto { get; set; }

        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult RelatorioHospedagemMes()
        {
            List<HospedagemMes> relatorio = IHospedagem.RelatorioHospedagemMes().ToList<HospedagemMes>();

            return View(relatorio);
        }

        public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Views/Relatorio"), "RelatorioHospedagem.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<HospedagemMes> cm = IHospedagem.RelatorioHospedagemMes().ToList<HospedagemMes>();

            ReportDataSource rd = new ReportDataSource("HospedagemDataSet", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }
    }
}