using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using LearnPolish.Phone.ViewModel;
using LearnPolish.Phone.Model;

namespace LearnPolish.Android
{
	[Activity(Label = "Polski Krok po Kroku", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private ViewModel _viewModel;
		private TextView labWord; 
		private Button buttonSwitchLanguage;
		private Button buttonTranslate;
		private EditText edit;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _viewModel = new ViewModel(new StreamReader(Assets.Open("Dictionary.txt")).ReadToEnd());

            labWord = FindViewById<TextView>(Resource.Id.labWord);
			buttonSwitchLanguage = FindViewById<Button>(Resource.Id.buttonSwitchLanguage);
			buttonTranslate = FindViewById<Button>(Resource.Id.buttonTranslate);
            edit = FindViewById<EditText>(Resource.Id.latText);

			labWord.SetBackgroundColor (new global::Android.Graphics.Color (0, 0, 0));

            buttonSwitchLanguage.Text =
                  _viewModel.WordLanguage == Language.Ukrainian
                      ? "UA => PL"
                      : "PL => UA";

			labWord.Text = _viewModel.Word;

            buttonSwitchLanguage.Click += (sender, args) =>
            {
                _viewModel.SwitchLanguagesMethod();

                buttonSwitchLanguage.Text =
                    _viewModel.WordLanguage == Language.Ukrainian
                        ? "UA => PL"
                        : "PL => UA";

				labWord.Text = _viewModel.Word;
            };		

			buttonTranslate.Click += (object sender, EventArgs e) => 
			{
				_viewModel.Translation = edit.Text;

				if (_viewModel.CanCheckTranslation)
				{
					_viewModel.CheckTranslation();

					if (_viewModel.Wrong)
					{
						labWord.SetBackgroundColor (new global::Android.Graphics.Color (255, 0, 0));
						edit.Text = _viewModel.Translation;
					}
					else
					{
						labWord.Text = _viewModel.Word;
						labWord.SetBackgroundColor (new global::Android.Graphics.Color (0, 0, 0));
						edit.Text = string.Empty;
					}				
				}
			};

			edit.KeyPress += (object sender, View.KeyEventArgs e) => 
			{
				e.Handled = false;

				if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
				{
					_viewModel.Translation = edit.Text;

					if (_viewModel.CanCheckTranslation)
					{
						_viewModel.CheckTranslation();

						if (_viewModel.Wrong)
						{
							labWord.SetBackgroundColor (new global::Android.Graphics.Color (255, 0, 0));
							edit.Text = _viewModel.Translation;
						}
						else
						{
							labWord.Text = _viewModel.Word;
							labWord.SetBackgroundColor (new global::Android.Graphics.Color (0, 0, 0));
							edit.Text = string.Empty;
						}
					}

					e.Handled = true;
				}
			};
        }
    }
}

