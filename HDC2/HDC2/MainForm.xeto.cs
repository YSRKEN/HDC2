using System;
using Eto.Forms;
using Eto.Serialization.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Reactive.Bindings;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HDC2
{	
	public class MainForm : Form
	{
		// コンストラクタ
		public MainForm() {
			XamlReader.Load(this);
			this.Menu.ApplicationMenu.Text = "ファイル(&F)";
			this.Menu.HelpMenu.Text = "ヘルプ(&H)";
			// ViewModelを登録する
			var model = new MainModel();
			DataContext = model;
		}
		// 設定メニュー
		protected void OptionCommand(object sender, EventArgs e) {
			// スタブ
		}
		// 終了メニュー
		protected void CloseCommand(object sender, EventArgs e) {
			Application.Instance.Quit();
		}
		// バージョン情報メニュー
		protected void AboutCommand(object sender, EventArgs e)
		{
			var dialog = new AboutDialog() {
				ProgramName = "大破率計算機II", Title = "バージョン情報", License = "MIT License",
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

		// プロパティ
		public ReactiveCommand ClickMeCommand { get; }
		public ReactiveProperty<int> MaxHitPoint { get; } = new ReactiveProperty<int>(31);
		public ReactiveProperty<int> Defense { get; } = new ReactiveProperty<int>(59);
		public ReactiveProperty<int> NowHitPoint { get; } = new ReactiveProperty<int>(31);
		public ReadOnlyReactiveCollection<string> GrafParameterList { get; }

		// 実行するメソッド
		private void ClickMe() {
			MessageBox.Show("I was clicked!");
		}

		// コンストラクタ
		public MainModel() {
			// の設定
			{
				var list = new List<string> { "A", "B", "C" };
				var oc = new ObservableCollection<string>(list);
				GrafParameterList = oc.ToReadOnlyReactiveCollection();
			}
			// コマンドの設定
			ClickMeCommand = new ReactiveCommand();
			ClickMeCommand.Subscribe(ClickMe);
		}
	}
}
