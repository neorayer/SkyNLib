using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sky.UI
{
	/// <summary>
	/// 注意，目前只支持Time Only的模式，不支持Date的编辑显示
	/// </summary>
	public class DateTimePickerColumn : DataGridViewColumn
	{
		public DateTimePickerColumn()
			: base(new DateTimePickerCell())
		{
		}

		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				// Ensure that the cell used for the template is a CalendarCell.
				if (value != null &&
					!value.GetType().IsAssignableFrom(typeof(DateTimePickerCell)))
				{
					throw new InvalidCastException("Must be a DateTimePickerCell");
				}
				base.CellTemplate = value;
			}
		}
	}

	public class DateTimePickerCell : DataGridViewTextBoxCell
	{

		public DateTimePickerCell()
			: base()
		{
		}

		public override void InitializeEditingControl(int rowIndex, object
			initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			// Set the value of the editing control to the current cell value.
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			DateTimePickerEditingControl ctl = DataGridView.EditingControl as DateTimePickerEditingControl;
			// Use the default row value when Value property is null.
			if (this.Value == null)
			{
				ctl.Value = (DateTime)this.DefaultNewRowValue;
			}
			else
			{
				ctl.Value = (DateTime)this.Value;
			}
		}

		public override Type EditType
		{
			get
			{
				// Return the type of the editing control that CalendarCell uses.
				return typeof(DateTimePickerEditingControl);
			}
		}

		public override Type ValueType
		{
			get
			{
				// Return the type of the value that CalendarCell contains.

				return typeof(DateTime);
			}
		}

		public override object DefaultNewRowValue
		{
			get
			{
				// Use the current date and time as the default value.
				return DateTime.Now;
			}
		}
	}

	class DateTimePickerEditingControl : DateTimePicker, IDataGridViewEditingControl
	{
		DataGridView dataGridView;
		private bool valueChanged = false;
		int rowIndex;

		public DateTimePickerEditingControl()
		{
			this.Format = DateTimePickerFormat.Time;
			this.ShowUpDown = true;
		}

		public object EditingControlFormattedValue
		{
			get
			{
				return this.Value.ToString();
			}
			set
			{
				if (value is String)
				{
					try
					{
						// This will throw an exception of the string is 
						// null, empty, or not in the format of a date.
						this.Value = DateTime.Parse((String)value);
					}
					catch
					{
						// In the case of an exception, just use the 
						// default value so we're not left with a null
						// value.
						this.Value = DateTime.Now;
					}
				}
			}
		}

		public object GetEditingControlFormattedValue(
			DataGridViewDataErrorContexts context)
		{
			return EditingControlFormattedValue;
		}

		public void ApplyCellStyleToEditingControl(
			DataGridViewCellStyle dataGridViewCellStyle)
		{
			this.Font = dataGridViewCellStyle.Font;
			this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
			this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
		}

		public int EditingControlRowIndex
		{
			get
			{
				return rowIndex;
			}
			set
			{
				rowIndex = value;
			}
		}

		public bool EditingControlWantsInputKey(
			Keys key, bool dataGridViewWantsInputKey)
		{
			// Let the DateTimePicker handle the keys listed.
			switch (key & Keys.KeyCode)
			{
				case Keys.Left:
				case Keys.Up:
				case Keys.Down:
				case Keys.Right:
				case Keys.Home:
				case Keys.End:
				case Keys.PageDown:
				case Keys.PageUp:
					return true;
				default:
					return !dataGridViewWantsInputKey;
			}
		}

		public void PrepareEditingControlForEdit(bool selectAll)
		{
			// No preparation needs to be done.
		}

		public bool RepositionEditingControlOnValueChange
		{
			get
			{
				return false;
			}
		}

		public DataGridView EditingControlDataGridView
		{
			get
			{
				return dataGridView;
			}
			set
			{
				dataGridView = value;
			}
		}

		public bool EditingControlValueChanged
		{
			get
			{
				return valueChanged;
			}
			set
			{
				valueChanged = value;
			}
		}

		public Cursor EditingPanelCursor
		{
			get
			{
				return base.Cursor;
			}
		}

		protected override void OnValueChanged(EventArgs eventargs)
		{
			valueChanged = true;
			this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
			base.OnValueChanged(eventargs);
		}
	}

}
