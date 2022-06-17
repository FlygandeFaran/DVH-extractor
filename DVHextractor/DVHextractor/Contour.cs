using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VMS.TPS.Common.Model.API;

namespace DVHextractor
{
    public class Contour
    {
        private string m_name;
        private string m_Input;
        private bool m_IsAbsInput;
        private bool m_IsDoseInput;
        private bool m_isAbsOutput;
        private Structure m_structure;
        private string m_inputUnit;

        public string InputName
        {
            get { return m_inputUnit.Substring(0, 1) + m_Input + m_inputUnit.Substring(1); }
        }
        public string InputUnit
        {
            get { return m_inputUnit; }
            set { m_inputUnit = value; }
        }
        public Structure Structure
        {
            get { return m_structure; }
            set { m_structure = value; }
        }
        public string Input
        {
            get { return m_Input; }
            set { m_Input = value; }
        }
        public bool isAbsOutput
        {
            get { return m_isAbsOutput; }
            set { m_isAbsOutput = value; }
        }
        public bool IsAbsInput
        {
            get { return m_IsAbsInput; }
            set { m_IsAbsInput = value; }
        }
        public bool IsDoseInput
        {
            get { return m_IsDoseInput; }
            set { m_IsDoseInput = value; }
        }
        public string Name
        {
            get { return m_name; }
        }
        public Contour(Structure structure)
        {
            m_name = structure.Id;
            m_structure = structure;
            m_Input = "";
        }
        public Contour(Contour other)
        {
            m_name = other.Name;
            m_Input = other.Input;
            m_inputUnit = other.InputUnit;
            m_IsAbsInput = other.IsAbsInput;
            m_isAbsOutput = other.isAbsOutput;
            m_IsDoseInput = other.IsDoseInput;
            m_structure = other.Structure;
    }
        public bool CheckInput(string strInput)
        {
            double result;
            bool ok = double.TryParse(strInput, out result);
            return ok;
        }
        public void AddInput(string input, string inputUnit)
        {
            if (input != "")
            {
                m_Input = input;
                m_inputUnit = inputUnit;
                if (m_inputUnit.Contains("D"))
                {
                    m_IsDoseInput = true;
                    if (m_inputUnit.Substring(1, 2).Equals("cc"))
                        m_IsAbsInput = true;
                    else
                        m_IsAbsInput = false;

                    if (m_inputUnit.Contains("[Gy]"))
                        m_isAbsOutput = true;
                    else
                        m_isAbsOutput = false;
                }
                else if (m_inputUnit.Contains("V"))
                {
                    m_IsDoseInput = false;
                    if (m_inputUnit.Substring(1, 2).Equals("Gy"))
                        m_IsAbsInput = true;
                    else
                        m_IsAbsInput = false;

                    if (m_inputUnit.Contains("[cc]"))
                        m_isAbsOutput = true;
                    else
                        m_isAbsOutput = false;
                }
            }
                /*m_IsAbsInput = isAbsInput;
                m_IsDoseInput = isDoseInput;
                m_Input = input;
                if (input != "")
                {
                    m_DVH = new IonDVH(Convert.ToDouble(m_Input), m_IsDoseInput, isAbsInput);
                    if (m_IsDoseInput && isAbsInput)
                        m_Input = "D" + m_Input + "cc";
                    else if (!m_IsDoseInput && isAbsInput)
                        m_Input = "V" + m_Input + "Gy";
                    else if(m_IsDoseInput && !isAbsInput)
                        m_Input = "D" + m_Input + "%";
                    else if(!m_IsDoseInput && !isAbsInput)
                        m_Input = "V" + m_Input + "%";

                }
                else
                    m_DVH = new IonDVH(); // Om input är tom*/
        }
        //public string StrInput()
        //{
        //    string strOut = "";



        //    /*
        //    string strOut = "";
        //    if (!m_IsDoseInput && !m_IsAbsInput)
        //        strOut = string.Format("{0,-8} {1,-8} {2,-8} {3,-8}", DVH.Input, string.Empty, string.Empty, string.Empty);
        //    else if (m_IsDoseInput && !m_IsAbsInput)
        //        strOut = string.Format("{0,-8} {1,-8} {2,-8} {3,-8}", string.Empty, DVH.Input, string.Empty, string.Empty);
        //    else if (!m_IsDoseInput && m_IsAbsInput)
        //        strOut = string.Format("{0,-8} {1,-8} {2,-8} {3,-8}", string.Empty, string.Empty, DVH.Input, string.Empty);
        //    else if (m_IsDoseInput && m_IsAbsInput)
        //        strOut = string.Format("{0,-8} {1,-8} {2,-8} {3,-8}", string.Empty, string.Empty, string.Empty, DVH.Input);*/
        //    return strOut;
        //}
        public override string ToString()
        {
            return m_name;
        }
    }
}