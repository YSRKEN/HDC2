using System;
using Eto.Forms;
using Eto.Serialization.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HDC2
{	
	public class MainForm : Form
	{
		public MainForm() {
			XamlReader.Load(this);
			// ViewModel��o�^����
			var model = new MainModel();
			DataContext = model;
		}

		protected void HandleClickMe(object sender, EventArgs e)
		{
			// ���j���[(ButtonMenuItem)��Command�v���p�e�B�Ɂu"{Binding ClickMeCommand}"�v��
			// �ݒ肵�Ă������Ȃ������̂ŁA�R�[�h�r�n�C���h�Ɍ���������DataContext�𒼐ڒ@���r��
			(DataContext as MainModel).ClickMeCommand.Execute(sender);
		}

		protected void HandleAbout(object sender, EventArgs e)
		{
			new AboutDialog().ShowDialog(this);
		}

		protected void HandleQuit(object sender, EventArgs e)
		{
			Application.Instance.Quit();
		}
	}

	public class MainModel : INotifyPropertyChanged
	{
		// �v���p�e�B
		public ICommand ClickMeCommand { get; private set; }
		public string LabelText { get; private set; }

		// ���s���郁�\�b�h
		private void ClickMe(object sender, EventArgs e) {
			MessageBox.Show("I was clicked!");
		}

		private void OnPropertyChanged([CallerMemberName] string memberName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
		public event PropertyChangedEventHandler PropertyChanged;

		// �R���X�g���N�^
		public MainModel() {
			ClickMeCommand = new Command(ClickMe);
			LabelText = "Some Content";
		}
	}
}
