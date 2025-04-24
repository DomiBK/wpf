public static class PDFExporter
{
    public static void Export(Raport raport)
    {
        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Verdana", 12);

        double y = 40;
        gfx.DrawString($"Tytuł: {raport.Tytul}", font, XBrushes.Black, new XPoint(40, y));
        y += 30;
        gfx.DrawString($"Data: {raport.DataStworzenia.ToShortDateString()}", font, XBrushes.Black, new XPoint(40, y));
        y += 40;

        foreach (var el in raport.Elementy)
        {
            gfx.DrawString($"- {el.Nazwa}: {el.Szczegoly}", font, XBrushes.Black, new XPoint(40, y));
            y += 25;
        }

        var path = $"Raport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
        document.Save(path);
        Process.Start("explorer", path);
    }
}
