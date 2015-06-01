using System;

namespace LearnPolish.Model
{
    public class Word
    {
        public Word(string name, Language language)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            Name = name.ToLower();
            Language = language;
        }

        public string Name { get; private set; }

        public Language Language { get; private set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var word = obj as Word;
            if ((object)word == null)
            {
                return false;
            }

            return (Name == word.Name) && (Language == word.Language);
        }

        public bool Equals(Word word)
        {
            if ((object)word == null)
            {
                return false;
            }

            return (Name == word.Name) && (Language == word.Language);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Language.GetHashCode();
        }

        public static bool operator ==(Word left, Word right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (((object)left == null) || ((object)right == null))
            {
                return false;
            }


            return left.Name == right.Name && left.Language == right.Language;
        }

        public static bool operator !=(Word left, Word right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, Language);
        }
    }
}
