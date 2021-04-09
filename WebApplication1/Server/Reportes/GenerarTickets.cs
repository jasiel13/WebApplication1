using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Globalization;
//using BarcodeLib;

namespace BlazorApp.Server.Reportes
{
    public class GenerarTickets
    {
        public class PdfPageEventsSinMarcaDeAgua : PdfPageEventHelper
        {

            public void Detalle(ref PdfPTable table, BaseColor FontColor, string Value, int Align, float Size = 8f, int Tipo = 0, int ColSpan = 1, int VerticalAlignment = 4)
            {
                PdfPCell cell = this.PhraseCell(new Phrase(Value, FontFactory.GetFont("Arial", Size, Tipo, FontColor)), Align);
                if (ColSpan > 1)
                {
                    cell.Colspan = ColSpan;
                }
                cell.VerticalAlignment = VerticalAlignment;
                table.AddCell(cell);
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                PdfPTable table = new PdfPTable(1);
                Phrase phrase = null;
                PdfPCell cell = null;
                BaseColor color = new BaseColor(0x15, 0x7b, 0xff);
                BaseColor fontColor = new BaseColor(0xa9, 0xa9, 0xa9);
                BaseColor color3 = new BaseColor(220, 220, 220);
                BaseColor bLACK = BaseColor.BLACK;
                string fecha = "";
                int px = 180;
                table = new PdfPTable(2)
                {
                    DefaultCell = { Border = 0 },
                    TotalWidth = (document.PageSize.Width - document.LeftMargin) - document.RightMargin,
                    LockedWidth = true
                };
                float[] relativeWidths = new float[] { 0.15f, 0.85f };
                table.SetWidths(relativeWidths);

                this.Detalle(ref table, bLACK, "Pag: " + document.PageNumber.ToString(), 0, 8f, 1, 2, 4);

                PdfPTable table2 = new PdfPTable(4)
                {
                    DefaultCell = { Border = 0 },
                    TotalWidth = table.TotalWidth * 0.80f,
                    LockedWidth = true
                };
                table2.SetWidths(new float[] { 0.25f, 0.25f, 0.25f, 0.25f });

                phrase = new Phrase();
                cell = this.PhraseCell(phrase, 8);
                cell.VerticalAlignment = 4;
                cell.Colspan = 4;

                table.TotalWidth = 480;
                table2.AddCell(cell);
                table2.AddCell(cell);
                table.AddCell(table2);
                table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height, writer.DirectContent);
            }

            private PdfPCell PhraseCell(Phrase phrase, int align) =>
                new PdfPCell(phrase)
                {
                    BorderColor = BaseColor.WHITE,
                    VerticalAlignment = 4,
                    HorizontalAlignment = align,
                    PaddingBottom = 2f,
                    PaddingTop = 0f
                };

            public void TitDetalle(ref PdfPTable table, BaseColor FontColor, BaseColor BackColor, BaseColor LineColor, string Title, int Align, float Size = 8f, int Tipo = 0, int ColSpan = 1, int VerticalAlignment = 4)
            {
                PdfPCell cell = this.PhraseCell(new Phrase(Title, FontFactory.GetFont("Arial", Size, Tipo, FontColor)), Align);
                cell.BorderColorBottom = LineColor;
                cell.BorderColorLeft = BackColor;
                cell.BorderColorRight = BackColor;
                cell.BorderColorTop = BackColor;
                cell.BorderWidthBottom = 0.4f;
                cell.BackgroundColor = BackColor;
                if (ColSpan > 1)
                {
                    cell.Colspan = ColSpan;
                }
                cell.VerticalAlignment = VerticalAlignment;
                table.AddCell(cell);
            }
        }
        private static PdfPCell PhraseCell(Phrase phrase, int align) =>
             new PdfPCell(phrase)
             {
                 BorderColor = BaseColor.WHITE,
                 VerticalAlignment = 4,
                 HorizontalAlignment = align,
                 PaddingBottom = 2f,
                 PaddingTop = 0f
             };
        public static void Detalle(ref PdfPTable table, BaseColor FontColor, string Value, int Align, float Size = 8f, int Tipo = 0, int ColSpan = 1, int VerticalAlignment = 4)
        {
            PdfPCell cell = PhraseCell(new Phrase(Value, FontFactory.GetFont("Arial", Size, Tipo, FontColor)), Align);
            if (ColSpan > 1)
            {
                cell.Colspan = ColSpan;
            }
            cell.VerticalAlignment = VerticalAlignment;
            //Quitamos el Borde de cada celda de la Tabla
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
        }








        public static byte[] repMes(string titulo)
        {
            Rectangle tamaño = new Rectangle(612, 790);
            Document document = new Document(PageSize.LETTER, 10f, 10f, 60f, 0f);
            iTextSharp.text.Font font = FontFactory.GetFont("Arial", 8f, 0, BaseColor.BLACK);

            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter instance = PdfWriter.GetInstance(document, stream);

                PdfPageEventsSinMarcaDeAgua events = new PdfPageEventsSinMarcaDeAgua();
                instance.PageEvent = events;

                Phrase phrase = null;
                PdfPTable table = null;
                PdfPCell cell = null;
                BaseColor lineColor = new BaseColor(0x15, 0x7b, 0xff);
                BaseColor fontColor = new BaseColor(0xff, 0xff, 0xff);
                BaseColor plata = new BaseColor(0xa9, 0xa9, 0xa9);
                BaseColor backColor = new BaseColor(220, 220, 220);
                BaseColor bLACK = BaseColor.BLACK;

                document.Open();


                table = new PdfPTable(10);

                table.SetWidths(new float[] { .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f });

                table.TotalWidth = 480;
                table.LockedWidth = true;
                table.SpacingBefore = 4f;
                table.HorizontalAlignment = 0;



                Detalle(ref table, bLACK, "\n", 0, 30f, 0, 10, 4);
                Detalle(ref table, bLACK, titulo, 1, 9f, 0, 10, 4);
                Detalle(ref table, bLACK, "\n", 0, 9f, 0, 10, 4);

                document.Add(table);
                document.Close();

                byte[] arch = stream.ToArray();
                stream.Close();
                return arch;

            }

        }
    }
}
