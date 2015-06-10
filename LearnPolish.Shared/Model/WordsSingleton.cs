using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LearnPolish.Phone.Model
{
    public sealed class Words
    {
        private readonly IDictionary<Language, IDictionary<Word, IList<Word>>> _dictionary;

        public Words()
        {
            _dictionary = new Dictionary<Language, IDictionary<Word, IList<Word>>>
            {
                { Language.Ukrainian, new Dictionary<Word, IList<Word>>() },
                { Language.Polish, new Dictionary<Word, IList<Word>>() }
            };
        }

        public IEnumerable<Word> GetWords()
        {
            return _dictionary.SelectMany(x => x.Value.Keys);
        }

        public IEnumerable<Word> GetWords(Language language)
        {
            return _dictionary[language].Select(x => x.Key);
        }

        public IEnumerable<Word >Translate(Word word, Language language)
        {
            return _dictionary[word.Language][word];
        }

        public void ReadWords(string rowWords)
        {
            using (var reader = new StringReader(rowWords))
            {
                while (true)
                {
                    var line = reader.ReadLine();

                    if (line == null)
                    {
                        return;
                    }

                    var words = line.Split(';');
                    var ukrainian = new Word(words[0], Language.Ukrainian);
                    var polish = new Word(words[1], Language.Polish);

                    AddWord(ukrainian, polish);
                    AddWord(polish, ukrainian);
                }
            }
        }

        private void AddWord(Word word, Word translation)
        {
            if (!_dictionary[word.Language].ContainsKey(word))
            {
                _dictionary[word.Language].Add(word, new List<Word>());
            }

            if (_dictionary[word.Language][word].Contains(translation))
            {
                throw new InvalidOperationException(string.Format("Word '{0}' already contains translation '{1}'", word.Name, translation.Name));
            }

            _dictionary[word.Language][word].Add(translation);
        }
    }
}
