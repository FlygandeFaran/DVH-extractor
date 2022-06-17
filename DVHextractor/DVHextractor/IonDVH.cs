using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using DoseUncertaintyDiff;

namespace DVHextractor
{
    public class IonDVH
    {
        private bool m_isDose;
        private bool m_isAbsInput;
        private bool m_isAbsOutput;
        private IonPlanSetup m_plan;
        private PlanUncertainty m_uPlan;
        private DVHData dvhDataAbs;
        private DVHData dvhDataRel;
        private Contour m_contour;
        private double m_input;
        private bool m_isTableAbs;
        private double m_outputValue;
        
        public bool IsTableAbs
        {
            get { return m_isTableAbs; }
            set { m_isTableAbs = value; }
        }
        public double Input
        {
            get { return m_input; }
            set { m_input = value; }
        }
        public double OutputValue
        {

            get { return m_outputValue; }
            set { m_outputValue = value; }
            /*get
            {
                if (m_isDose)
                    return m_dose;
                else
                    return m_volume;
            }*/
        }
        public double Mean
        {
            get
            {
                if (m_isTableAbs)
                    return dvhDataAbs.MeanDose.Dose;
                else
                    return dvhDataRel.MeanDose.Dose;
            }
        }
        public double Min
        {
            get
            {
                if (m_isTableAbs)
                    return dvhDataAbs.MinDose.Dose;
                else
                    return dvhDataRel.MinDose.Dose;
            }
        }
        public double Max
        {
            get
            {
                if (m_isTableAbs)
                    return dvhDataAbs.MaxDose.Dose;
                else
                    return dvhDataRel.MaxDose.Dose;
            }
        }
        /*public DVHData DVHdata
        {
            get { return dvhData; }
            set { dvhData = value; }
        }*/
        //public IonDVH() { }
        public IonDVH(Contour contour, IonPlanSetup plan)
        {
            m_plan = plan;
            m_contour = contour;
            GenerateDVH();

            if (!string.IsNullOrEmpty(m_contour.Input))
            {
                m_input = Convert.ToDouble(m_contour.Input);
                m_isDose = m_contour.IsDoseInput;
                m_isAbsInput = m_contour.IsAbsInput;
                m_isAbsOutput = m_contour.isAbsOutput;
                AddInput();
            }
        }
        public IonDVH(Contour contour, IonPlanSetup plan, PlanUncertainty uPlan)
        {
            m_uPlan = uPlan;
            m_contour = contour;
            GenerateUDVH();

            if (!string.IsNullOrEmpty(m_contour.Input))
            {
                m_input = Convert.ToDouble(m_contour.Input);
                m_isDose = m_contour.IsDoseInput;
                m_isAbsInput = m_contour.IsAbsInput;
                m_isAbsOutput = m_contour.isAbsOutput;
                AddUInput();
            }
        }
        private void GenerateDVH()
        {
            dvhDataAbs = m_plan.GetDVHCumulativeData(m_contour.Structure, DoseValuePresentation.Absolute, VolumePresentation.Relative, 0.001);
            
            dvhDataRel = m_plan.GetDVHCumulativeData(m_contour.Structure, DoseValuePresentation.Relative, VolumePresentation.Relative, 0.001);
        }
        private void GenerateUDVH()
        {
            dvhDataAbs = m_uPlan.GetDVHCumulativeData(m_contour.Structure, DoseValuePresentation.Absolute, VolumePresentation.Relative, 0.001);
            
            dvhDataRel = m_uPlan.GetDVHCumulativeData(m_contour.Structure, DoseValuePresentation.Relative, VolumePresentation.Relative, 0.001);
        }
        private void AddInput()
        {
            DoseValue doseInput;
            if (!m_isDose)
            {
                if (m_isAbsInput)
                    doseInput = new DoseValue(m_input, DoseValue.DoseUnit.Gy);
                else
                    doseInput = new DoseValue(m_input, DoseValue.DoseUnit.Percent);
                if (m_isAbsOutput)
                    OutputValue = DvhExtensions.GetVolumeAtDose(m_plan, m_contour.Structure, doseInput, VolumePresentation.AbsoluteCm3);
                else
                    OutputValue = DvhExtensions.GetVolumeAtDose(m_plan, m_contour.Structure, doseInput, VolumePresentation.Relative);
            }
            else if (m_isDose)
            {
                if (m_isAbsInput)
                {
                    if (m_isAbsOutput)
                        doseInput = DvhExtensions.GetDoseAtVolume(m_plan, m_contour.Structure, m_input, VolumePresentation.AbsoluteCm3, DoseValuePresentation.Absolute);
                    else
                        doseInput = DvhExtensions.GetDoseAtVolume(m_plan, m_contour.Structure, m_input, VolumePresentation.AbsoluteCm3, DoseValuePresentation.Relative);
                }
                else
                {
                    if (m_isAbsOutput)
                        doseInput = DvhExtensions.GetDoseAtVolume(m_plan, m_contour.Structure, m_input, VolumePresentation.Relative, DoseValuePresentation.Absolute);
                    else
                        doseInput = DvhExtensions.GetDoseAtVolume(m_plan, m_contour.Structure, m_input, VolumePresentation.Relative, DoseValuePresentation.Relative);
                }
                m_outputValue = doseInput.Dose;
            }
        }
        private void AddUInput()
        {
            DoseValue doseInput;
            DoseValue doseOutput;
            DVHData dvhData;
            if (!m_isDose)
            {
                if (m_isAbsInput)
                {
                    doseInput = new DoseValue(m_input, DoseValue.DoseUnit.Gy);
                    if (m_isAbsOutput)
                    {
                        dvhData = m_uPlan.GetDVHCumulativeData(m_contour.Structure, DoseValuePresentation.Absolute, VolumePresentation.AbsoluteCm3, 0.001);
                        OutputValue = DvhExtensions.VolumeAtDose(dvhData, doseInput.Dose);
                    }
                    else
                    {
                        dvhData = m_uPlan.GetDVHCumulativeData(m_contour.Structure, DoseValuePresentation.Absolute, VolumePresentation.Relative, 0.001);
                        OutputValue = DvhExtensions.VolumeAtDose(dvhData, doseInput.Dose);
                    }
                }
                else
                {
                    doseInput = new DoseValue(m_input, DoseValue.DoseUnit.Percent);
                    if (m_isAbsOutput)
                    {
                        dvhData = m_uPlan.GetDVHCumulativeData(m_contour.Structure, DoseValuePresentation.Relative, VolumePresentation.AbsoluteCm3, 0.001);
                        OutputValue = DvhExtensions.VolumeAtDose(dvhData, doseInput.Dose);
                    }
                    else
                    {
                        dvhData = m_uPlan.GetDVHCumulativeData(m_contour.Structure, DoseValuePresentation.Relative, VolumePresentation.Relative, 0.001);
                        OutputValue = DvhExtensions.VolumeAtDose(dvhData, doseInput.Dose);
                    }
                }
            }
            else
            {
                doseOutput = new DoseValue();
                if (m_isAbsInput)
                {
                    if (m_isAbsOutput)
                    {
                        dvhData = m_uPlan.GetDVHCumulativeData(m_contour.Structure, DoseValuePresentation.Absolute, VolumePresentation.AbsoluteCm3, 0.001);
                        doseOutput = DvhExtensions.DoseAtVolume(dvhData, m_input);
                    }
                    else
                    {
                        dvhData = m_uPlan.GetDVHCumulativeData(m_contour.Structure, DoseValuePresentation.Relative, VolumePresentation.AbsoluteCm3, 0.001);
                        doseOutput = DvhExtensions.DoseAtVolume(dvhData, m_input);
                    }
                }
                else
                {
                    if (m_isAbsOutput)
                    {
                        dvhData = m_uPlan.GetDVHCumulativeData(m_contour.Structure, DoseValuePresentation.Absolute, VolumePresentation.Relative, 0.001);
                        doseOutput = DvhExtensions.DoseAtVolume(dvhData, m_input);
                    }
                    else
                    {
                        dvhData = m_uPlan.GetDVHCumulativeData(m_contour.Structure, DoseValuePresentation.Relative, VolumePresentation.Relative, 0.001);
                        doseOutput = DvhExtensions.DoseAtVolume(dvhData, m_input);
                    }
                }
                OutputValue = doseOutput.Dose;
            }
        }
    }
}