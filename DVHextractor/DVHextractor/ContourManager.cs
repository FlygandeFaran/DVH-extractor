using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DVHextractor
{
    public class ContourManager
    {
        private List<Contour> contourList;//list of contours
        private List<string> uniqueContourList;//list of contours
        private List<string> inputValues; //lista över alla inputs för alla konturer // kanske måste ändra till string för att funka

        public List<Contour> ContourList
        {
            get { return contourList; }
            set { contourList = value; }
        }
        public List<string> UniqueContourList
        {
            get
            {
                listOfAllUniqueContours();
                return uniqueContourList;
            }
        }
        public List<string> InputValues
        {
            get
            {
                InputList();
                return inputValues;
            }
        }
        public ContourManager()
        {
            Initialize();
        }
        private void Initialize()
        {
            contourList = new List<Contour>();
        }
        public void AddContour(Contour contourIn)
        {
            contourList.Add(contourIn);
        }
        public Contour GetContour(int index)
        {
            bool ok = CheckIndex(index);
            if (ok)
                return contourList[index];
            return null;
        }
        public void DeleteContour(int index)
        {
            bool ok = CheckIndex(index);
            if (ok)
                contourList.RemoveAt(index);
        }
        public bool CheckIndex(int index)
        {
            bool ok = false;
            if (index >= 0 && index < contourList.Count)
                ok = true;
            return ok;
        }
        public void ClearContours()
        {
            contourList.Clear();
        }
        private void listOfAllUniqueContours()
        {
            List<string> duplicateList = contourList.GroupBy(x => x.Name).Where(g => g.Count() > 1).Select(x => x.Key).ToList();
            List<string> singleList = contourList.GroupBy(x => x.Name).Where(g => g.Count() == 1).Select(x => x.Key).ToList();
            uniqueContourList = new List<string>();
            uniqueContourList.AddRange(duplicateList);
            uniqueContourList.AddRange(singleList);
        }
        private void InputList()
        {
            inputValues = new List<string>(); // skapar lista över alla inputs, ska vara samma för alla planer

            foreach (Contour tempContour in contourList)
                if(tempContour.Input != "")
                    if (!inputValues.Contains(tempContour.InputName))
                        inputValues.Add(tempContour.InputName); // fyller lista med inputs
        }
    }
}