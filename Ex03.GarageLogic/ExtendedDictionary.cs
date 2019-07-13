using System;

namespace Ex03.GarageLogic
{
    public class ExtendedDictionary
    {
        private readonly string r_TagName;
        private readonly string r_Question;
        private readonly eAnswerTypes r_ExpectedAnswerType;
        private readonly int r_FirstEnumIndex;
        private readonly int r_LastEnumIndex;
        private readonly float r_MaxValue;

        public ExtendedDictionary(string i_TagName, string i_Question, eAnswerTypes i_ExpectedAnswerType)
        {
            r_TagName = i_TagName;
            r_Question = i_Question;
            r_ExpectedAnswerType = i_ExpectedAnswerType;
        }

        public ExtendedDictionary(string i_TagName, string i_Question, eAnswerTypes i_ExpectedAnswerType, float i_MaxVaue)
        {
            r_TagName = i_TagName;
            r_Question = i_Question;
            r_ExpectedAnswerType = i_ExpectedAnswerType;
            r_MaxValue = i_MaxVaue;
        }

        // Constructor for Enums
        public ExtendedDictionary(string i_TagName, string i_Question, eAnswerTypes i_ExpectedAnswerType, int i_FirstEnumIndex, int i_LastEnumIndex)
        {
            r_TagName = i_TagName;
            r_Question = i_Question;
            r_ExpectedAnswerType = i_ExpectedAnswerType;
            r_FirstEnumIndex = i_FirstEnumIndex;
            r_LastEnumIndex = i_LastEnumIndex;
        }

        public string Tag
        {
            get
            {
                return r_TagName;
            }
        }

        public string Question
        {
            get
            {
                return r_Question;
            }
        }

        public eAnswerTypes AnswerType
        {
            get
            {
                return r_ExpectedAnswerType;
            }
        }

        public int FirstValidIndex
        {
            get
            {
                return r_FirstEnumIndex;
            }
        }

        public int LastValidIndex
        {
            get
            {
                return r_LastEnumIndex;
            }
        }

        public float MaxValue
        {
            get
            {
                return r_MaxValue;
            }
        }
    }
}
