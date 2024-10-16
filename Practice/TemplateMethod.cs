using System;

public abstract class ReportGenerator
{
    public void GenerateReport()
    {
        GatherData();
        FormatData();
        CreateReport();
        SaveReport();
        NotifyCompletion();
    }

    protected abstract void GatherData();
    protected abstract void FormatData();
    protected abstract void CreateReport();

    protected virtual void SaveReport()
    {
        Console.WriteLine("Отчет сохранен.");
    }

    protected virtual void NotifyCompletion()
    {
        Console.WriteLine("Генерация отчета завершена.");
    }
}

public class PdfReport : ReportGenerator
{
    protected override void GatherData()
    {
        Console.WriteLine("Сбор данных для PDF отчета.");
    }

    protected override void FormatData()
    {
        Console.WriteLine("Форматирование данных для PDF отчета.");
    }

    protected override void CreateReport()
    {
        Console.WriteLine("Создание PDF отчета.");
    }
}

public class ExcelReport : ReportGenerator
{
    protected override void GatherData()
    {
        Console.WriteLine("Сбор данных для Excel отчета.");
    }

    protected override void FormatData()
    {
        Console.WriteLine("Форматирование данных для Excel отчета.");
    }

    protected override void CreateReport()
    {
        Console.WriteLine("Создание Excel отчета.");
    }

    protected override void SaveReport()
    {
        Console.WriteLine("Excel отчет сохранен.");
    }
}

public class HtmlReport : ReportGenerator
{
    protected override void GatherData()
    {
        Console.WriteLine("Сбор данных для HTML отчета.");
    }

    protected override void FormatData()
    {
        Console.WriteLine("Форматирование данных для HTML отчета.");
    }

    protected override void CreateReport()
    {
        Console.WriteLine("Создание HTML отчета.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ReportGenerator pdfReport = new PdfReport();
        pdfReport.GenerateReport();

        Console.WriteLine();

        ReportGenerator excelReport = new ExcelReport();
        excelReport.GenerateReport();

        Console.WriteLine();

        ReportGenerator htmlReport = new HtmlReport();
        htmlReport.GenerateReport();
    }
}
