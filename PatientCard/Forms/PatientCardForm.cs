﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PatientCard.Data;
using PatientCard.Logic;

namespace PatientCard.Forms
{
    public partial class PatientCardForm : Form
    {
        public PatientCardForm(ClinicDataSet.PatientCardsRow row)
        {
            InitializeComponent();

            // text only
            textBoxFirstName.Validating += TextBoxTextOnlyValidating;
            textBoxLastName.Validating += TextBoxTextOnlyValidating;
            textBoxMiddleName.Validating += TextBoxTextOnlyValidating;

            // text empty
            textBoxFirstName.Validating += TextBoxEmptyValidating;
            textBoxLastName.Validating += TextBoxEmptyValidating;
            textBoxMiddleName.Validating += TextBoxEmptyValidating;

            textBoxAddress.Validating += TextBoxEmptyValidating;
            textBoxSocial.Validating += TextBoxEmptyValidating;
            textBoxWork.Validating += TextBoxEmptyValidating;

            // validated
            textBoxFirstName.Validated += ControlValidated;
            textBoxLastName.Validated += ControlValidated;
            textBoxMiddleName.Validated += ControlValidated;

            textBoxAddress.Validated += ControlValidated;
            textBoxSocial.Validated += ControlValidated;
            textBoxWork.Validated += ControlValidated;

            Row = row;
        }

        public EditMode EditMode { get; set; }
        public ClinicDataSet.PatientCardsRow Row { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            if (EditMode == EditMode.ReadOnly)
            {
                Utility.LockTextBoxes(this);
            }
            if (EditMode != EditMode.CreateNew)
            {
                dateFill.Value = Row.Created;
                dateBirth.Value = Row.BirthDate;

                textBoxFirstName.Text = Row.FirstName;
                textBoxLastName.Text = Row.LastName;
                textBoxMiddleName.Text = Row.MiddleName;

                textBoxAddress.Text = Row.Address;
                textBoxPhone.Text = Row.Phone;
                textBoxSocial.Text = Row.SocialStatus;
                textBoxWork.Text = Row.Work;

                radioMale.Checked = (Row.Gender == "M");
                radioFemale.Checked = (Row.Gender == "F");
            }
            base.OnLoad(e);
        }

        private void multiText_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var editText = new EditTextForm(((TextBox) sender).Text);
            if (editText.ShowDialog() == DialogResult.OK)
            {
                ((TextBox) sender).Text = editText.ResultText;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (EditMode == EditMode.CreateNew)
            {
                Row.Created = dateFill.Value;
            }
            if (EditMode != EditMode.ReadOnly)
            {
                if (!ValidateChildren())
                {
                    DialogResult = DialogResult.None;
                    return;
                }

                Row.BirthDate = dateBirth.Value;

                Row.FirstName = textBoxFirstName.Text;
                Row.LastName = textBoxLastName.Text;
                Row.MiddleName = textBoxMiddleName.Text;

                Row.Address = textBoxAddress.Text;
                Row.Phone = textBoxPhone.Text;
                Row.SocialStatus = textBoxSocial.Text;
                Row.Work = textBoxWork.Text;

                Row.Gender = radioMale.Checked ? "M" : "F";
            }
        }

        private void buttonResearchs_Click(object sender, EventArgs e)
        {
            var form = new ResearchForm();
            form.ShowDialog();
        }

        private void buttonHistory_Click(object sender, EventArgs e)
        {
            var form = new HistoryForm(Row.CardId);
            form.ShowDialog();
        }

        private void TextBoxEmptyValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox) sender).Text))
            {
                e.Cancel = true;
                errorProvider.SetError((Control) sender, "Пустое поле");
            }
        }

        private void TextBoxTextOnlyValidating(object sender, CancelEventArgs e)
        {
            if (Regex.IsMatch(((TextBox) sender).Text, @"[A-Za-z]+"))
            {
                e.Cancel = true;
                errorProvider.SetError((Control) sender, "Только текст");
            }
        }

        private void ControlValidated(object sender, EventArgs e)
        {
            errorProvider.SetError((Control) sender, "");
        }
    }
}
