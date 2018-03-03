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
			// ViewModelを登録する
			var model = new MainModel();
			DataContext = model;
		}

		protected void HandleClickMe(object sender, EventArgs e)
		{
			// メニュー(ButtonMenuItem)のCommandプロパティに「"{Binding ClickMeCommand}"」と
			// 設定しても動かなかったので、コードビハインドに見せかけてDataContextを直接叩く荒業
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

		// プロパティ
		public ReactiveCommand ClickMeCommand { get; }
		public ReactiveProperty<string> LabelText { get; } = new ReactiveProperty<string>("Some Content");

		// 実行するメソッド
		private void ClickMe() {
			MessageBox.Show("I was clicked!");
		}

		// コンストラクタ
		public MainModel() {
			ClickMeCommand = new ReactiveCommand();
			ClickMeCommand.Subscribe(ClickMe);
		}
	}
}
