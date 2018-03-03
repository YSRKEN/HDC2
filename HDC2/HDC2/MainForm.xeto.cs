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
		// �R���X�g���N�^
		public MainForm() {
			XamlReader.Load(this);
			this.Menu.ApplicationMenu.Text = "�t�@�C��(&F)";
			this.Menu.HelpMenu.Text = "�w���v(&H)";
			// ViewModel��o�^����
			var model = new MainModel();
			DataContext = model;
		}
		// �ݒ胁�j���[
		protected void OptionCommand(object sender, EventArgs e) {
			// �X�^�u
		}
		// �I�����j���[
		protected void CloseCommand(object sender, EventArgs e) {
			Application.Instance.Quit();
		}
		// �o�[�W������񃁃j���[
		protected void AboutCommand(object sender, EventArgs e)
		{
			var dialog = new AboutDialog() {
				ProgramName = "��j���v�Z�@II", Title = "�o�[�W�������", License = "MIT License",
				Website = new Uri("https://github.com/YSRKEN/HDC2"), WebsiteLabel = "GitHub" };
			dialog.ShowDialog(this);
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
