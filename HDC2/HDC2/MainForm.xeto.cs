using System;
using Eto.Forms;
using Eto.Serialization.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Reactive.Bindings;

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
		private void OnPropertyChanged([CallerMemberName] string memberName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
		public event PropertyChangedEventHandler PropertyChanged;

		// �v���p�e�B
		public ReactiveCommand ClickMeCommand { get; }
		public ReactiveProperty<string> LabelText { get; } = new ReactiveProperty<string>("Some Content");

		// ���s���郁�\�b�h
		private void ClickMe() {
			MessageBox.Show("I was clicked!");
		}

		// �R���X�g���N�^
		public MainModel() {
			ClickMeCommand = new ReactiveCommand();
			ClickMeCommand.Subscribe(ClickMe);
		}
	}
}
