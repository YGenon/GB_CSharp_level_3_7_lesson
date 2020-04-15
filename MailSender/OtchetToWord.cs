using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace MailSender
{
    class OtchetToWord
    {
		private DocX _document;
		private Font _fontFamily = new Font("Times New Roman");
		private double _fontSizeText = 12;
		private double _fontSizeTitle = 14;
		private double _spacing = 1.5;
		private string _filename;

		public OtchetToWord(string filename)
		{
			_filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
				filename);// Создаем файл на рабочем столе

			try
			{
				using (DocX document = DocX.Create(_filename))
				{
					document.Save();
				}

			}
			catch (Exception e)
			{
				throw new Exception(@"Произошла ошибка при создании документа. Подробнее: " + e.Message);
			}
		}

		public void Export(ArrayList list)
		{

			using (DocX document = DocX.Load(_filename))
			{
				_document = document;

				_document.MarginLeft = 85; // 3 cm
				_document.MarginRight = 28.3f;// 1 cm
				_document.MarginTop = 28.3f;
				_document.MarginBottom = 28.3f;
				AddTitle("Отчет по адресатам. \r\n");
								
				for (int i = 0; i < list.Count; i++)
				{
					CreateParagraph().Append(list[i].ToString());
				}							
			
				PageBreak();
				document.Save();
				Process.Start(_filename);

				//MessageBox.Show("Файл отчета создан - " + _filename);
			}
		}


		private void PageBreak()
		{
			if (_document == null) return;
			Paragraph pageBreak = _document.InsertParagraph();
			pageBreak.InsertPageBreakAfterSelf();
		}

		private void AddTitle(string vale)
		{
			if (_document == null) return;
			Paragraph title = _document.InsertParagraph();
			title.Append(vale).Font(_fontFamily).FontSize(_fontSizeTitle).Bold().Alignment = Alignment.center;
		}

		private void AddTextLineToParagraph(Paragraph paragraph, string vale)
		{
			if (_document == null) return;
			paragraph.AppendLine(vale).Font(_fontFamily).FontSize(_fontSizeText);
		}

		private void AddTextAppendToParagraph(Paragraph paragraph, string vale)
		{
			if (_document == null) return;
			paragraph.Append(vale).Font(_fontFamily).FontSize(_fontSizeText);
		}

		
		private Paragraph CreateParagraph()
		{
			Paragraph paragraph = _document.InsertParagraph();
			paragraph.Spacing(_spacing);
			return paragraph;
		}
	}

}

