﻿using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MailSender.Service;

namespace MailSender.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private readonly IDataAccessService _dataService;
		private ObservableCollection<Emails> _emails = new ObservableCollection<Emails>();

		public ObservableCollection<Emails> Emails
		{
			get => _emails;
			set
			{
				Set(ref _emails, value);
				//if (Equals(_emails, value)) return;
				//_emails = value;
				//RaisePropertyChanged(nameof(Emails));
			}
		}
		private Emails _currentEmail = new Emails();
		public Emails CurrentEmail
		{
			get => _currentEmail;
			set => Set(ref _currentEmail, value);
		}

		public RelayCommand<Emails> SaveEmailCommand { get; }
		public RelayCommand ReadAllMailsCommand { get; }
		public RelayCommand Click_Otchet { get; }		

		public MainWindowViewModel(IDataAccessService dataService)
		{
			_dataService = dataService;
			 
			ReadAllMailsCommand = new RelayCommand(GetEmails);

			Click_Otchet = new RelayCommand(CreateOtchetWord); 			

			SaveEmailCommand = new RelayCommand<Emails>(SaveEmail);
			
		}

		private void SaveEmail(Emails email)
		{

			email.Id = _dataService.CreateEmail(email);
			if (email.Id == 0) return;
			Emails.Add(email);
		}

		//private void GetEmails() => Emails = _dataService.GetEmails();
		private void GetEmails()
		{
			if (searchText.Length < 1) { Emails = _dataService.GetEmails(); }
		}


		private string searchText = "";

		public string SetSearchText
		{
			set 
			{ 
				searchText = value;
				if (searchText.Length > 1) { SearhEmails(searchText);}
				
			}
			
		}
		private void SearhEmails(string searchText)
		{
			Emails.Clear();
			Emails = _dataService.SelectEmails(searchText);
			//MessageBox.Show("Ищем");
		}

		/// <summary>
		/// Делает в Word отчет по получателям сообщений
		/// </summary>
		private void CreateOtchetWord()
		{
			ArrayList listRecipients = new ArrayList();
			Emails = _dataService.GetEmails();
			foreach (var item in Emails)
			{
				listRecipients.Add(item.Name + " - " + item.Email);
			}

			var export = new OtchetToWord("report_Second.docx");
			export.Export(listRecipients);
			//MessageBox.Show("Отчет создан");
		}
	}
}
