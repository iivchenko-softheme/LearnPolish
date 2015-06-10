using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LearnPolish.Phone.Model;

namespace LearnPolish.Phone.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private int _tryCount = 3;
        private readonly Random _random = new Random();

        private readonly Words _wordsSingleton;

        private IList<Word> _words;

        private string _word; // Word to translate.
        private string _translation;// Translation of the word.

        private Language _wordLanguage;
        private Language _translationLanguage;

        private bool _wrong;

        public ViewModel(string words)
        {
            _words = new List<Word>();
            _wordsSingleton = new Words();
            _wordsSingleton.ReadWords(words);

            WordLanguage = Language.Polish;
            TranslationLanguage = Language.Ukrainian;

            _wrong = false;

            NextWord();
        }

        public IEnumerable<Language> Languages { get; set; }

        public string Word
        {
            get
            {
                return _word;
            }

            set
            {
                _word = value.ToLower();

                OnPropertyChanged();
            }
        }

        public Language WordLanguage
        {
            get
            {
                return _wordLanguage;
            }

            set
            {
                _wordLanguage = value;

                _words = new List<Word>();

                NextWord();

                OnPropertyChanged();
            }
        }

        public string Translation
        {
            get
            {
                return _translation;
            }

            set
            {
                _translation = value.ToLower();

                OnPropertyChanged();

                OnPropertyChanged("CanCheckTranslation");
            }
        }

        public Language TranslationLanguage
        {
            get
            {
                return _translationLanguage;
            }

            set
            {
                _translationLanguage = value;

                OnPropertyChanged();
            }
        }

        public bool Wrong
        {
            get
            {
                return _wrong;
                
            }

            set
            {
                _wrong = value;
                
                OnPropertyChanged();
            }
        }

        public void CheckTranslation()
        {
            var word = new Word(_word, _wordLanguage);
            var actualTranslation = new Word(_translation, _translationLanguage);
            var expectedTranslations = _wordsSingleton.Translate(word, actualTranslation.Language).ToList();
            
            if (expectedTranslations.Any(x => x == actualTranslation))
            {
                NextWord();

                Wrong = false;
            }
            else
            {
                _tryCount--;

                if (_tryCount <= 0)
                {
                    _tryCount = 3;
                    Translation = expectedTranslations[_random.Next(expectedTranslations.Count)].Name;

                    _words.Insert(7, _words.First());
                }

                Wrong = true;
            }
        }
        
        public bool CanCheckTranslation
        {
            get { return !string.IsNullOrWhiteSpace(_translation); }
        }

        public void SwitchLanguagesMethod()
        {
            var temp = TranslationLanguage;

            TranslationLanguage = WordLanguage;

            WordLanguage = temp;
        }

        private void NextWord()
        {
            _tryCount = 3;

            if (!_words.Any())
            {
                var tempWords = _wordsSingleton.GetWords(WordLanguage).ToList();

                while (tempWords.Any())
                {
                    var index = _random.Next(tempWords.Count);
                    _words.Add(tempWords[index]);
                    tempWords.RemoveAt(index);
                }
            }
            else
            {
                _words.RemoveAt(0);
            }

            Word = _words[0].Name;
            Translation = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
