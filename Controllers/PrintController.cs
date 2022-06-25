using Dtos;
using Microsoft.AspNetCore.Mvc;
using ESC_POS_USB_NET.Printer;

namespace Print.Controllers
{
  [ApiController]
  [Route("print")]
    public class PrintController : ControllerBase
    {
      private IConfiguration configuration { set; get; }
      public PrintController(IConfiguration configuration)
      {
        this.configuration = configuration;
      }

        // POST: print
        [HttpPost]
        public IActionResult print(PrintDto dto)
        {
            Printer printer = new Printer(this.configuration.GetValue("PrinterName", "RP58 Printer"), "UTF-8");
            // get current date
            string date = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string line = "";
            for (int i = 0; i < 32; i++)
            {
                line += "=";
            }
            printer.AlignCenter();
            printer.Append(dto.meta.store_name);
            printer.Append(dto.meta.store_address);
            printer.Append("Telp/WA: " + dto.meta.phone);
            printer.Separator(' ');
            printer.AlignLeft();
            printer.Append("#" + dto.meta.trx_id);
            printer.Append(line);
            printer.Append("Tgl: " + date);
            printer.Append(line);
            int total = 0;
            foreach (var product in dto.products)
            {
                printer.Append(product.name);
                printer.Append(String.Format("{0,10:#,#.##}x{1,10:#,#.##} {2,10:#,#.##} ", product.qty, product.price, product.qty * (product.price - product.discount)));
                if (product.discount > 0)
                {
                    printer.Append(String.Format("Disc.      {0,10:#,#.##}", product.discount));
                }
                total += product.qty * (product.price - product.discount);
            }
            printer.Append(String.Format("{0,10} {1,10} {2,10}", "", "", "----------"));
            printer.Append(String.Format("{0,10} {1,10} {2,10:#,#.##}", "", "Diskon Gl.", dto.discount));
            printer.Append(String.Format("{0,10} {1,10} {2,10:#,#.##}", "", "Total", total));
            printer.Append(String.Format("{0,10} {1,10} {2,10:#,#.##}", "", "Tunai", dto.cash));
            printer.Append(String.Format("{0,10} {1,10} {2,10}", "", "", "----------"));
            printer.Append(String.Format("{0,10} {1,10} {2,10:#,#.##}", "", "Kembali", dto.cash - total));
            printer.AlignCenter();
            printer.Separator(' ');
            printer.Append("Terima Kasih");
            printer.FullPaperCut();
            printer.PrintDocument();
            return Ok();
        }
    }
}