using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VMS.TPS.Common.Model.API;

namespace DVHextractor
{
    public partial class MainForm : Form
    {
        private ContourManager cmAvailableContours;
        private ContourManager cmSelectedContours;
        //private bool m_IsAbsInput;
        //private bool m_IsDoseInput;
        private bool m_IsAbsTable;
        private string additionalInput;
        private IonDVH DVH;
        private DataTable absDT;
        private DataTable relDT;
        private CSV csv;
        private IonPlanSetup m_ionPlan;

        public static void Main(IonPlanSetup ionplan)
        {
            System.Windows.Forms.Application.Run(new MainForm(ionplan));
        }
        public MainForm(IonPlanSetup ionplan)
        {
            m_ionPlan = ionplan;
            InitializeComponent();
            InitializeGUI(ionplan);

        }
        private void InitializeGUI(IonPlanSetup ionplan)
        {
            csv = new CSV();
            cmAvailableContours = new ContourManager(); // konturlista för available konturer, lite överflödig men whatever
            cmSelectedContours = new ContourManager();
            lsbDoseOrVol.Items.AddRange(Enum.GetNames(typeof(Unit)));
            lsbDoseOrVol.SelectedIndex = (int)Unit.Volume;

            foreach (Course m_course in ionplan.Course.Patient.Courses)
                lbListOfCourses.Items.Add(m_course);

            lbListOfCourses.SelectedItem = ionplan.Course;
            UpdateListOfPlans();
            lsListOfPlans.SelectedItem = ionplan;
            
            lsbAbsOrRelInput.Items.AddRange(Enum.GetNames(typeof(UnitType)));
            lsbAbsOrRelInput.SelectedIndex = (int)UnitType.Relative;

            lsbAbsOrRelOutput.Items.AddRange(Enum.GetNames(typeof(UnitType)));
            lsbAbsOrRelOutput.SelectedIndex = (int)UnitType.Relative;

            lsbAbsOrRelTable.Items.AddRange(Enum.GetNames(typeof(UnitType)));
            lsbAbsOrRelTable.SelectedIndex = (int)UnitType.Relative;
            //m_IsAbsTable = false;
            UpdateUnit(); // uppdaterar enheten på input
        }
        private void UpdateListOfPlans()
        {
            lsListOfPlans.Items.Clear();
            foreach (Course item in lbListOfCourses.SelectedItems)
            {
                foreach (IonPlanSetup m_plan in item.IonPlanSetups)
                {
                    lsListOfPlans.Items.Add(m_plan);
                }
            }
        }
        private void UpdateTable()
        {
            dgvDVHtable.DataSource = null;
            if (lsbAbsOrRelTable.SelectedIndex == (int)UnitType.Absolute && absDT != null)
                dgvDVHtable.DataSource = absDT;
            else if (lsbAbsOrRelTable.SelectedIndex == (int)UnitType.Relative && relDT != null)
                dgvDVHtable.DataSource = relDT;
        }
        private void UpdateListOfStructures()
        {
            lsbAvailableContours.Items.Clear();
            cmAvailableContours.ClearContours();
            foreach (IonPlanSetup m_plan in lsListOfPlans.SelectedItems)
            {
                foreach (Structure struktur in m_plan.StructureSet.Structures)
                {
                    if (cmAvailableContours.ContourList.FirstOrDefault(cont => cont.Name.Equals(struktur.Id)) == null)
                    {
                        lsbAvailableContours.Items.Add(new Contour(struktur));
                        cmAvailableContours.AddContour(new Contour(struktur));
                    }
                }
            }
        }
        private void UpdateUnit()
        {
            string inputUnit = "";

            if (lsbAbsOrRelInput.SelectedIndex == (int)UnitType.Relative)
                inputUnit += "%";

            if (lsbDoseOrVol.SelectedIndex == (int)Unit.Dose)
            {
                lblVolOrDose.Text = "D";
                if (lsbAbsOrRelInput.SelectedIndex == (int)UnitType.Absolute)
                    inputUnit += "cc";
                if (lsbAbsOrRelOutput.SelectedIndex == (int)UnitType.Absolute)
                    inputUnit += " [Gy]";
            }

            else if (lsbDoseOrVol.SelectedIndex == (int)Unit.Volume)
            {
                lblVolOrDose.Text = "V";
                if (lsbAbsOrRelInput.SelectedIndex == (int)UnitType.Absolute)
                    inputUnit += "Gy";
                if (lsbAbsOrRelOutput.SelectedIndex == (int)UnitType.Absolute)
                    inputUnit += " [cc]";
            }
            
            if (lsbAbsOrRelOutput.SelectedIndex == (int)UnitType.Relative)
                inputUnit += " [%]";
            lblInputUnit.Text = inputUnit;

            additionalInput = lblVolOrDose.Text + inputUnit;
        }
        private void UpdateSelectedListBox()
        {
            lsbSelectedContours.Items.Clear();
            lsbAdditionalValues.Items.Clear();
            
            for (int i = 0; i < cmSelectedContours.ContourList.Count; i++)
            {
                Contour tempContour = new Contour(cmSelectedContours.GetContour(i));
                lsbSelectedContours.Items.Add(tempContour.Name);
                if (!string.IsNullOrEmpty(tempContour.Input))
                    lsbAdditionalValues.Items.Add(tempContour.InputName);
                else
                    lsbAdditionalValues.Items.Add(tempContour.Input);
            }
        }
        private void UpdateTextBox()
        {
            if (!cbIsKeepValue.Checked)
                txtInput.Clear();
        }
        private void UpdateSelectedContourList(Contour tempContour) // Lägger till kontur i "Selected Contour"
        {
            //tempContour.AddInput(input, isAbsInput, isDoseInput);
            cmSelectedContours.AddContour(tempContour);
            UpdateSelectedListBox();
            UpdateTextBox();
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            relDT = new DataTable();
            absDT = new DataTable();

            try
            {
                m_IsAbsTable = false;
                relDT = GenerateTable(relDT);
                m_IsAbsTable = true;
                absDT = GenerateTable(absDT);

                UpdateTable();
            }
            catch (Exception a)
            {
                MessageBox.Show("Something went wrong\n\nRemeber, you cannot have opened the 'dose statistics' tab. If you have, reload the patient and try again");
                MessageBox.Show(a.ToString());
            }
        }
        private DataTable GenerateTable(DataTable dt)
        {
            dt.Columns.Add("Plan", typeof(string)); // lägger till plan som rubrik
            dt.Columns.Add("Contours", typeof(string)); // lägger till kontur som rubrik
            dt.Columns.Add("Min", typeof(string));
            dt.Columns.Add("Max", typeof(string));
            dt.Columns.Add("Mean", typeof(string));

            bool isUplan = false;

            if (cmSelectedContours.InputValues.Count > 0)
                foreach (string column in cmSelectedContours.InputValues) // lägger till inputsen som rubriker
                    dt.Columns.Add(column.ToString(), typeof(string));
            
            foreach (IonPlanSetup temp_plan in lsListOfPlans.SelectedItems) // går igenom alla valda planer
            {
                if (temp_plan.IsDoseValid)
                {
                    foreach (string contourName in cmSelectedContours.UniqueContourList) //varje unik kontur
                    {
                        Contour uniqContour = cmSelectedContours.ContourList.FirstOrDefault(s => s.Name.Equals(contourName));
                        PlanUncertainty temp_uPlan = temp_plan.PlanUncertainties.FirstOrDefault(plan => !plan.Id.Contains(" "));
                        if (temp_plan.PlanUncertainties.Count() > 0 && temp_uPlan != null && (rbAllUnCalc.Checked || rbExtremeUnCalc.Checked)) // Behövs för att lura Eclipse, måste räkna uPlan först! OBS UPLANS ÄR FAN LÖMSKA
                        {
                            if (temp_uPlan.Dose != null)
                            {
                                uniqContour.Structure = temp_plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName));
                                temp_uPlan.GetDVHCumulativeData(uniqContour.Structure, VMS.TPS.Common.Model.Types.DoseValuePresentation.Relative, VMS.TPS.Common.Model.Types.VolumePresentation.Relative, 0.001);
                                isUplan = true;
                            }
                        }
                        
                        if (temp_plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName)) != null)
                        {
                            object[] ContourInputs = GenerateData(temp_plan, contourName);

                            dt.Rows.Add(ContourInputs);
                            if (rbAllUnCalc.Checked && isUplan)
                            {
                                foreach (PlanUncertainty uPlan in temp_plan.PlanUncertainties)
                                {
                                    if (!uPlan.Id.Contains(" ")) // behövs för att eclipse skapar konstiga "extra" uPlaner med " "
                                    {
                                        object[] uContourInputs = GenerateUdata(temp_plan, uPlan, contourName);
                                        dt.Rows.Add(uContourInputs);
                                    }
                                }
                            }
                            else if (rbExtremeUnCalc.Checked && isUplan)
                            {
                                object[] maxUContourInputs = GenerateMaxOutliers(temp_plan, contourName);
                                object[] minUContourInputs = GenerateMinOutliers(temp_plan, contourName);
                                dt.Rows.Add(maxUContourInputs);
                                dt.Rows.Add(minUContourInputs);
                            }
                        }

                    }
                }
                else
                    MessageBox.Show("No calculated dose in " + temp_plan.Id);
            }
            return dt;
        }

        private object[] GenerateMaxOutliers(IonPlanSetup temp_plan, string contourName)
        {
            double max = 0;
            double min = 0;
            double mean = 0;
            double[] outputValue = new double[cmSelectedContours.InputValues.Count + 5];

            for (int i = 5; i < cmSelectedContours.InputValues.Count + 5; ++i)
            {
                outputValue[i] = 0;
            }


            object[] maxOutlier = new object[cmSelectedContours.InputValues.Count + 5];
            Contour uniqContour = cmSelectedContours.ContourList.FirstOrDefault(s => s.Name.Equals(contourName));
            maxOutlier[0] = "Max uPlan values";
            maxOutlier[1] = uniqContour.Name;
            foreach (PlanUncertainty uPlan in temp_plan.PlanUncertainties)
            {
                if (!uPlan.Id.Contains(" ")) // behövs för att eclipse skapar konstiga "extra" uPlaner med " "
                {
                    uniqContour.Structure = temp_plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName));
                    DVH = new IonDVH(uniqContour, temp_plan, uPlan);

                    if (m_IsAbsTable)
                        DVH.IsTableAbs = true;
                    else
                        DVH.IsTableAbs = false;

                    if (DVH.Min > min)
                    {
                        min = DVH.Min;
                        maxOutlier[2] = min.ToString("0.00") + " (" + uPlan.Id + ")";
                    }
                    if (DVH.Max > max)
                    {
                        max = DVH.Max;
                        maxOutlier[3] = max.ToString("0.00") + " (" + uPlan.Id + ")";
                    }
                    if (DVH.Mean > mean)
                    {
                        mean = DVH.Mean;
                        maxOutlier[4] = mean.ToString("0.00") + " (" + uPlan.Id + ")";
                    }

                    foreach (Contour tempContour in cmSelectedContours.ContourList) // varje kontur i "Selected contours", kan alltså återupprepa samma namn
                    {
                        if (tempContour.Name == contourName) // tar fram varje input från konturer med det här unika namnet
                        {
                            for (int i = 5; i < cmSelectedContours.InputValues.Count + 5; ++i)
                            {
                                if (!string.IsNullOrEmpty(tempContour.Input))
                                {
                                    if (tempContour.InputName == cmSelectedContours.InputValues.ElementAt(i - 5)) // om input för den här konturen hittas ansätts det, annars blir det null
                                    {
                                        tempContour.Structure = temp_plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName));
                                        DVH = new IonDVH(tempContour, temp_plan, uPlan);
                                        if (DVH.OutputValue > outputValue[i])
                                        {
                                            outputValue[i] = DVH.OutputValue;
                                            maxOutlier[i] = outputValue[i].ToString("0.00") + " (" + uPlan.Id + ")";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return maxOutlier;
        }
        private object[] GenerateMinOutliers(IonPlanSetup temp_plan, string contourName)
        {
            double max = 9999;
            double min = 9999;
            double mean = 9999;
            double[] outputValue = new double[cmSelectedContours.InputValues.Count + 5];

            for (int i = 5; i < cmSelectedContours.InputValues.Count + 5; ++i)
            {
                outputValue[i] = 9999;
            }
            
            object[] minOutlier = new object[cmSelectedContours.InputValues.Count + 5];
            Contour uniqContour = cmSelectedContours.ContourList.FirstOrDefault(s => s.Name.Equals(contourName));
            minOutlier[0] = "Min uPlan values";
            minOutlier[1] = uniqContour.Name;
            foreach (PlanUncertainty uPlan in temp_plan.PlanUncertainties)
            {
                if (!uPlan.Id.Contains(" ")) // behövs för att eclipse skapar konstiga "extra" uPlaner med " "
                {
                    uniqContour.Structure = temp_plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName));
                    DVH = new IonDVH(uniqContour, temp_plan, uPlan);

                    if (m_IsAbsTable)
                        DVH.IsTableAbs = true;
                    else
                        DVH.IsTableAbs = false;

                    if (DVH.Min < min)
                    {
                        min = DVH.Min;
                        minOutlier[2] = min.ToString("0.00") + " (" + uPlan.Id + ")";
                    }
                    if (DVH.Max < max)
                    {
                        max = DVH.Max;
                        minOutlier[3] = max.ToString("0.00") + " (" + uPlan.Id + ")";
                    }
                    if (DVH.Mean < mean)
                    {
                        mean = DVH.Mean;
                        minOutlier[4] = mean.ToString("0.00") + " (" + uPlan.Id + ")";
                    }


                    foreach (Contour tempContour in cmSelectedContours.ContourList) // varje kontur i "Selected contours", kan alltså återupprepa samma namn
                    {
                        if (tempContour.Name == contourName) // tar fram varje input från konturer med det här unika namnet
                        {
                            for (int i = 5; i < cmSelectedContours.InputValues.Count + 5; ++i)
                            {
                                if (!string.IsNullOrEmpty(tempContour.Input))
                                {
                                    if (tempContour.InputName == cmSelectedContours.InputValues.ElementAt(i - 5)) // om input för den här konturen hittas ansätts det, annars blir det null
                                    {
                                        tempContour.Structure = temp_plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName));
                                        DVH = new IonDVH(tempContour, temp_plan, uPlan);
                                        if (DVH.OutputValue < outputValue[i])
                                        {
                                            outputValue[i] = DVH.OutputValue;
                                            minOutlier[i] = outputValue[i].ToString("0.00") + " (" + uPlan.Id + ")";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return minOutlier;
        }

        private object[] GenerateData(IonPlanSetup plan, string contourName) // tar fram alla data från DVH
        {
            Contour uniqContour = cmSelectedContours.ContourList.FirstOrDefault(s => s.Name.Equals(contourName));
            uniqContour.Structure = plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName));
            DVH = new IonDVH(uniqContour, plan);
            object[] ContourInputs = GenerateMaxMinValues(plan.Id, uniqContour, false); // skapar object för tabell med plats för alla inputs + planID, konturnamn, min, max, medel

            foreach (Contour tempContour in cmSelectedContours.ContourList) // varje kontur i "Selected contours", kan alltså återupprepa samma namn
            {
                if (tempContour.Name == contourName) // tar fram varje input från konturer med det här unika namnet
                {
                    tempContour.Structure = plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName));
                    for (int i = 5; i < ContourInputs.Length; ++i)
                    {
                        if (!string.IsNullOrEmpty(tempContour.Input))
                        {
                            if (tempContour.InputName == cmSelectedContours.InputValues.ElementAt(i - 5)) // om input för den här konturen hittas ansätts det, annars blir det null
                            {
                                tempContour.Structure = plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName));
                                DVH = new IonDVH(tempContour, plan);
                                ContourInputs[i] = DVH.OutputValue.ToString("0.00");
                            }
                        }
                    }
                }
            }
            return ContourInputs;
        }
        private object[] GenerateUdata(IonPlanSetup plan, PlanUncertainty uPlan, string contourName) // tar fram alla data från DVH
        {
            Contour uniqContour = cmSelectedContours.ContourList.FirstOrDefault(s => s.Name.Equals(contourName));
            uniqContour.Structure = plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName));
            DVH = new IonDVH(uniqContour, plan, uPlan);
            object[] ContourInputs = GenerateMaxMinValues(uPlan.Id, uniqContour, false); // skapar object för tabell med plats för alla inputs + planID, konturnamn, min, max, medel

            foreach (Contour tempContour in cmSelectedContours.ContourList) // varje kontur i "Selected contours", kan alltså återupprepa samma namn
            {
                if (tempContour.Name == contourName) // tar fram varje input från konturer med det här unika namnet
                {
                    for (int i = 5; i < ContourInputs.Length; ++i)
                    {
                        if (!string.IsNullOrEmpty(tempContour.Input))
                        {
                            if (tempContour.InputName == cmSelectedContours.InputValues.ElementAt(i - 5)) // om input för den här konturen hittas ansätts det, annars blir det null
                            {
                                tempContour.Structure = plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName));
                                DVH = new IonDVH(tempContour, plan, uPlan);
                                ContourInputs[i] = DVH.OutputValue.ToString("0.00");
                            }
                        }
                    }
                }
            }
            return ContourInputs;
        }
        private object[] GenerateOutlyingUdata(IonPlanSetup plan, PlanUncertainty uPlan, string contourName) // tar fram alla data från DVH
        {
            Contour uniqContour = cmSelectedContours.ContourList.FirstOrDefault(s => s.Name.Equals(contourName));
            uniqContour.Structure = plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName));
            DVH = new IonDVH(uniqContour, plan, uPlan);
            object[] ContourInputs = GenerateMaxMinValues(uPlan.Id, uniqContour, false); // skapar object för tabell med plats för alla inputs + planID, konturnamn, min, max, medel

            foreach (Contour tempContour in cmSelectedContours.ContourList) // varje kontur i "Selected contours", kan alltså återupprepa samma namn
            {
                if (tempContour.Name == contourName) // tar fram varje input från konturer med det här unika namnet
                {
                    for (int i = 5; i < ContourInputs.Length; ++i)
                    {
                        if (!string.IsNullOrEmpty(tempContour.Input))
                        {
                            if (tempContour.InputName == cmSelectedContours.InputValues.ElementAt(i - 5)) // om input för den här konturen hittas ansätts det, annars blir det null
                            {
                                tempContour.Structure = plan.StructureSet.Structures.FirstOrDefault(cont => cont.Id.Equals(contourName));
                                DVH = new IonDVH(tempContour, plan, uPlan);
                                ContourInputs[i] = DVH.OutputValue.ToString("0.00");
                            }
                        }
                    }
                }
            }
            return ContourInputs;
        }
        private object[] GenerateMaxMinValues(string planId, Contour contour, bool isAbs)
        {
            if (m_IsAbsTable)
                DVH.IsTableAbs = true;
            else
                DVH.IsTableAbs = false;
            
            object[] contourInputs = new object[cmSelectedContours.InputValues.Count + 5];
            contourInputs[0] = planId;
            contourInputs[1] = contour.Name;
            contourInputs[2] = DVH.Min.ToString("0.00");
            contourInputs[3] = DVH.Max.ToString("0.00");
            contourInputs[4] = DVH.Mean.ToString("0.00");
            return contourInputs;
        }
        private void lsbDoseOrVol_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUnit();
        }
        private void lsbAbsOrRel_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUnit();
        }
        private void lsbAbsOrRelOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUnit();
        }
        private void btnAddContour_Click(object sender, EventArgs e)
        {
            UpdateSelectedContour();
        }

        private void UpdateSelectedContour()
        {
            if (lbListOfCourses.SelectedIndex != -1)
            {
                if (lsListOfPlans.SelectedIndex != -1)
                {
                    int index = lsbAvailableContours.SelectedIndex;
                    if (cmAvailableContours.CheckIndex(index))
                    {
                        Contour tempContour = new Contour(cmAvailableContours.GetContour(index));

                        if (tempContour.CheckInput(txtInput.Text) || txtInput.Text == string.Empty)
                        {
                            tempContour.AddInput(txtInput.Text, additionalInput);
                            UpdateSelectedContourList(tempContour);
                        }
                        else
                            MessageBox.Show("Enter a valid number");
                    }
                    else
                        MessageBox.Show("There is no contour selected among the available contours");
                }
                else
                    MessageBox.Show("No plan seleceted");
            }
            else
                MessageBox.Show("No course seleceted");
        }

        private void btnRemoveContour_Click(object sender, EventArgs e)
        {
            int index = lsbSelectedContours.SelectedIndex;
            if (cmSelectedContours.CheckIndex(index))
            {
                cmSelectedContours.DeleteContour(index);
                UpdateSelectedListBox();
                if (lsbSelectedContours.Items.Count > 0 && index < lsbSelectedContours.Items.Count)
                    lsbSelectedContours.SelectedIndex = index;
                else
                    lsbSelectedContours.SelectedIndex = index - 1;
            }
            else
                MessageBox.Show("There is no contour selected among the selected contours");
        }
        private void saveTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmSelectedContours.ContourList.Count > 0)
            {
                sfdlgSaveTemplate.InitialDirectory = @"\\SKVfile01.skandion.local\Gemensamdata$\Portal\Eclipse\Eclipse Scripting API\DVH script\Mallar";
                sfdlgSaveTemplate.Title = "Save template";
                sfdlgSaveTemplate.Filter = "All files (*.*)|*.*|csv files (*.csv)|*.csv";
                sfdlgSaveTemplate.FilterIndex = 2;

                if (sfdlgSaveTemplate.ShowDialog() == DialogResult.OK)
                {
                    string BrowserPath = sfdlgSaveTemplate.FileName;
                    List<string> listOfContours = new List<string>();
                    string strOut;
                    foreach (Contour tempContour in cmSelectedContours.ContourList)
                    {
                        strOut = tempContour.Name + ";" + tempContour.Input + ";" + tempContour.InputUnit + ";" + tempContour.IsAbsInput + ";" + tempContour.IsDoseInput + ";" + tempContour.isAbsOutput;
                        listOfContours.Add(strOut);
                    }
                    csv.WriteToCSV(BrowserPath, listOfContours);
                }
            }
            else
                MessageBox.Show("Selected Contour list is empty, cannot save as template");
        }
        private void loadTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmSelectedContours.ClearContours();
            ofdlgLoadTemplate.InitialDirectory = @"\\SKVfile01.skandion.local\Gemensamdata$\Portal\Eclipse\Eclipse Scripting API\DVH script\Mallar";
            ofdlgLoadTemplate.Title = "Selecte Template";
            ofdlgLoadTemplate.Filter = "All files (*.*)|*.*|csv files (*.csv)|*.csv";
            ofdlgLoadTemplate.FilterIndex = 2;

            //skapa contour och addinput()

            if (ofdlgLoadTemplate.ShowDialog() == DialogResult.OK)
            {
                string BrowserPath = ofdlgLoadTemplate.FileName;
                List<string> templateContourList = csv.ReadFromCSV(BrowserPath);
                string errorMsg = "";
                foreach (string line in templateContourList)
                {
                    var values = line.Split(';');

                    if (cmAvailableContours.ContourList.FirstOrDefault(s => s.Name.Equals(values[0])) != null)
                    {
                        Contour contour = new Contour(cmAvailableContours.ContourList.FirstOrDefault(s => s.Name.Equals(values[0]))); // varje kontur i "Selected contours", kan alltså återupprepa samma namn
                        contour.Input = values[1];
                        contour.InputUnit = values[2];
                        contour.IsAbsInput = Convert.ToBoolean(values[3]);
                        contour.IsDoseInput = Convert.ToBoolean(values[4]);
                        contour.isAbsOutput= Convert.ToBoolean(values[5]);

                        UpdateSelectedContourList(contour);
                    }
                    else
                        errorMsg += "Could not find " + values[0] + " " + values[1] + " " + values[2] + " in the list of available contours\n";
                }
                if (!string.IsNullOrEmpty(errorMsg))
                    MessageBox.Show(errorMsg);
            }
        }
        private void exportToCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sfdlgSaveToExcel.InitialDirectory = @"\\SKVfile01.skandion.local\Gemensamdata$\Portal\Eclipse\Eclipse Scripting API\DVH script\Data";
            sfdlgSaveToExcel.Title = "Choose location to save csv";
            sfdlgSaveToExcel.Filter = "All files (*.*)|*.*|csv files (*.csv)|*.csv";
            sfdlgSaveToExcel.FilterIndex = 2;
            if (absDT != null || relDT != null)
            {
                if (sfdlgSaveToExcel.ShowDialog() == DialogResult.OK)
                {
                    string BrowserPath = sfdlgSaveToExcel.FileName;

                    if (lsbAbsOrRelTable.SelectedIndex == (int)UnitType.Absolute && absDT != null)
                        csv.ExportToExcel(absDT, BrowserPath, true);
                    else if (lsbAbsOrRelTable.SelectedIndex == (int)UnitType.Relative && relDT != null)
                        csv.ExportToExcel(relDT, BrowserPath, false);
                }
                MessageBox.Show("Done!");
            }
            else
                MessageBox.Show("There is nothing to export");
        }
        private void addToExistingTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofdlgLoadTemplate.InitialDirectory = @"\\SKVfile01.skandion.local\Gemensamdata$\Portal\Eclipse\Eclipse Scripting API\DVH script\Mallar";
            ofdlgLoadTemplate.Title = "Selecte Template";
            ofdlgLoadTemplate.Filter = "All files (*.*)|*.*|csv files (*.csv)|*.csv";
            ofdlgLoadTemplate.FilterIndex = 2;

            //skapa contour och addinput()

            if (cmSelectedContours.ContourList.Count > 0)
            {
                if (ofdlgLoadTemplate.ShowDialog() == DialogResult.OK)
                {
                    string BrowserPath = ofdlgLoadTemplate.FileName;
                    List<string> templateContourList = csv.ReadFromCSV(BrowserPath);
                    List<string> listOfContours = new List<string>();
                    string strOut;
                    foreach (var contour in cmSelectedContours.ContourList)
                    {
                        if (templateContourList.FirstOrDefault(s => s.Split(';')[0].Equals(contour.Name)) == null)
                        {
                            strOut = contour.Name + ";" + contour.Input + ";" + contour.InputUnit + ";" + contour.IsAbsInput + ";" + contour.IsDoseInput + ";" + contour.isAbsOutput;
                            templateContourList.Add(strOut);
                            //listOfContours.Add(strOut);
                        }
                        csv.WriteToCSV(BrowserPath, templateContourList);
                    }
                    MessageBox.Show("Done!");
                }
            }
            else
                MessageBox.Show("Selected Contour list is empty, cannot save as template");
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            cmSelectedContours.ClearContours();
            UpdateSelectedListBox();
            UpdateTextBox();
        }

        private void lbListOfCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListOfPlans();
        }

        private void lsListOfPlans_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRadioButtons();
            if (lsListOfPlans.SelectedIndex != -1)
                UpdateListOfStructures();
        }

        private void UpdateRadioButtons()
        {
            if (lsListOfPlans.SelectedItems.Count == 1 && (IonPlanSetup)lsListOfPlans.SelectedItem == m_ionPlan)
            {
                rbAllUnCalc.Enabled = true;
                rbExtremeUnCalc.Enabled = true;
            }
            else
            {
                rbNone.Checked = true;
                rbAllUnCalc.Enabled = false;
                rbExtremeUnCalc.Enabled = false;
            }
        }

        private void lsbAbsOrRelTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dgvDVHtable.DataSource = null;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("OBS! Ladda om patient om du vill räkna robustdoser på annan plan.");
        }

        private void lsbAvailableContours_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UpdateSelectedContour();
        }
    }
}
