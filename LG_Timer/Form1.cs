using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace LG_Timer {
  public partial class MainForm : Form {
    private string _bak_loc = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\LGTimer";
    private string _user_loc = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Time_Records";

    public MainForm() {
      InitializeComponent();
      ManualInit();
      Directory.CreateDirectory(_bak_loc);
      Directory.CreateDirectory(_user_loc);
    }

    private Timer _timer;
    private double count = 0.0f;

    private void ManualInit() {
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.stopwatches = new SettableStopwatch[5];
      for (int x = 0; x < 5; x++) {
        stopwatches[x] = new SettableStopwatch(TimeSpan.Zero);
      }
          
      _timer = new Timer();
      _timer.Interval = 100;
      _timer.Tick += new EventHandler(_update);
      _timer.Start();

      playButton1.TabStop = false;
      playButton1.FlatStyle = FlatStyle.Flat;
      playButton1.FlatAppearance.BorderSize = 0;
      restartButton1.TabStop = false;
      restartButton1.FlatStyle = FlatStyle.Flat;
      restartButton1.FlatAppearance.BorderSize = 0;

      playButton2.TabStop = false;
      playButton2.FlatStyle = FlatStyle.Flat;
      playButton2.FlatAppearance.BorderSize = 0;
      restartButton2.TabStop = false;
      restartButton2.FlatStyle = FlatStyle.Flat;
      restartButton2.FlatAppearance.BorderSize = 0;

      playButton3.TabStop = false;
      playButton3.FlatStyle = FlatStyle.Flat;
      playButton3.FlatAppearance.BorderSize = 0;
      restartButton3.TabStop = false;
      restartButton3.FlatStyle = FlatStyle.Flat;
      restartButton3.FlatAppearance.BorderSize = 0;

      playButton4.TabStop = false;
      playButton4.FlatStyle = FlatStyle.Flat;
      playButton4.FlatAppearance.BorderSize = 0;
      restartButton4.TabStop = false;
      restartButton4.FlatStyle = FlatStyle.Flat;
      restartButton4.FlatAppearance.BorderSize = 0;

      playButton5.TabStop = false;
      playButton5.FlatStyle = FlatStyle.Flat;
      playButton5.FlatAppearance.BorderSize = 0;
      restartButton5.TabStop = false;
      restartButton5.FlatStyle = FlatStyle.Flat;
      restartButton5.FlatAppearance.BorderSize = 0;
    }

    private void toggleTopmost() {
      if (TopMost) {
        TopMost = false; 
        alwaysOnTopToolStripMenuItem.Checked = false;
      }
      else {
        TopMost = true;
        alwaysOnTopToolStripMenuItem.Checked = true;
      }
    }

    private void playButton1_Click(object sender, EventArgs e) {
      if (!stopwatches[0].IsRunning) {
        stopwatches[0].Start();
        timeDisplay1.ForeColor = Color.Green;
        playButton1.BackgroundImage = Properties.Resources.Pause_64;
      }
      else {
        stopwatches[0].Stop();
        timeDisplay1.ForeColor = Color.Black;
        playButton1.BackgroundImage = Properties.Resources.Play_64;
      }
    }

    private void restartButton1_Click(object sender, EventArgs e) {
      stopwatches[0].Reset();
      textBox1.Text = "";
      timeDisplay1.ForeColor = Color.Black;
      comboBox1.Text = "";
      playButton1.BackgroundImage = Properties.Resources.Play_64;
    }

    private void playButton2_Click(object sender, EventArgs e) {
      if (!stopwatches[1].IsRunning) {
        stopwatches[1].Start();
        timeDisplay2.ForeColor = Color.Green;
        playButton2.BackgroundImage = Properties.Resources.Pause_64;
      }
      else {
        stopwatches[1].Stop();
        timeDisplay2.ForeColor = Color.Black;
        playButton2.BackgroundImage = Properties.Resources.Play_64;
      }
    }

    private void restartButton2_Click(object sender, EventArgs e) {
      stopwatches[1].Reset();
      textBox2.Text = "";
      timeDisplay2.ForeColor = Color.Black;
      comboBox2.Text = "";
      playButton2.BackgroundImage = Properties.Resources.Play_64;
    }

    private void playButton3_Click(object sender, EventArgs e) {
      if (!stopwatches[2].IsRunning) {
        stopwatches[2].Start();
        timeDisplay3.ForeColor = Color.Green;
        playButton3.BackgroundImage = Properties.Resources.Pause_64;
      }
      else {
        stopwatches[2].Stop();
        timeDisplay3.ForeColor = Color.Black;
        playButton3.BackgroundImage = Properties.Resources.Play_64;
      }
    }

    private void restartButton3_Click(object sender, EventArgs e) {
      stopwatches[2].Reset();
      textBox3.Text = "";
      timeDisplay3.ForeColor = Color.Black;
      comboBox3.Text = "";
      playButton3.BackgroundImage = Properties.Resources.Play_64;
    }

    private void playButton4_Click(object sender, EventArgs e) {
      if (!stopwatches[3].IsRunning) {
        stopwatches[3].Start();
        timeDisplay4.ForeColor = Color.Green;
        playButton4.BackgroundImage = Properties.Resources.Pause_64;
      }
      else {
        stopwatches[3].Stop();
        timeDisplay4.ForeColor = Color.Black;
        playButton4.BackgroundImage = Properties.Resources.Play_64;
      }
    }

    private void restartButton4_Click(object sender, EventArgs e) {
      stopwatches[3].Reset();
      textBox4.Text = "";
      timeDisplay4.ForeColor = Color.Black;
      comboBox4.Text = "";
      playButton4.BackgroundImage = Properties.Resources.Play_64;
    }

    private void playButton5_Click(object sender, EventArgs e) {
      if (!stopwatches[4].IsRunning) {
        stopwatches[4].Start();
        timeDisplay5.ForeColor = Color.Green;
        playButton5.BackgroundImage = Properties.Resources.Pause_64;
      }
      else {
        stopwatches[4].Stop();
        timeDisplay5.ForeColor = Color.Black;
        playButton5.BackgroundImage = Properties.Resources.Play_64;
      }
    }

    private void restartButton5_Click(object sender, EventArgs e) {
      stopwatches[4].Reset();
      textBox5.Text = "";
      timeDisplay5.ForeColor = Color.Black;
      comboBox5.Text = "";
      playButton5.BackgroundImage = Properties.Resources.Play_64;
    }

    public static double CeilingTo(double value, double interval) {
      var remainder = value % interval;
      return remainder > 0 ? value + (interval - remainder) : value;
    }

    private double ToBillable(SettableStopwatch stopwatch) {
      double billable = 0.0;
      if(stopwatch.ElapsedTimeSpan.TotalHours == 0.0f) {
        billable = 0.0f;
      }
      else if (stopwatch.ElapsedTimeSpan.TotalHours < .05f && stopwatch.ElapsedTimeSpan.TotalHours > 0.0f) {
        billable = 0.1f;
      }
      else {
        billable = CeilingTo((stopwatch.ElapsedTimeSpan.TotalHours), .05);        
      }
      return billable;
    }

    private void _update(object sender, EventArgs e) {
      if (_running())
        _backup();

      timeDisplay1.Text = ToBillable(stopwatches[0]).ToString("F");
      auxDisplay1.Text = stopwatches[0].ElapsedTimeSpan.ToString(@"hh\:mm\:ss\:ff");
      timeDisplay2.Text = ToBillable(stopwatches[1]).ToString("F");
      auxDisplay2.Text = stopwatches[1].ElapsedTimeSpan.ToString(@"hh\:mm\:ss\:ff");
      timeDisplay3.Text = ToBillable(stopwatches[2]).ToString("F");
      auxDisplay3.Text = stopwatches[2].ElapsedTimeSpan.ToString(@"hh\:mm\:ss\:ff");
      timeDisplay4.Text = ToBillable(stopwatches[3]).ToString("F");
      auxDisplay4.Text = stopwatches[3].ElapsedTimeSpan.ToString(@"hh\:mm\:ss\:ff");
      timeDisplay5.Text = ToBillable(stopwatches[4]).ToString("F");
      auxDisplay5.Text = stopwatches[4].ElapsedTimeSpan.ToString(@"hh\:mm\:ss\:ff");
    }

    private void _restore() {
      if (File.Exists(_bak_loc + "\\Billable_Time_Backup.bak")) {
        TimerRecord restore = BinarySerialization.ReadFromBinaryFile<TimerRecord>(_bak_loc + "\\Billable_Time_Backup.bak");
        comboBox1.Text = restore.record1.clientName;
        stopwatches[0].ElapsedTimeSpan = restore.record1.actualTime;
        textBox1.Text = restore.record1.notes;

        comboBox2.Text = restore.record2.clientName;
        stopwatches[1].ElapsedTimeSpan = restore.record2.actualTime;
        textBox2.Text = restore.record2.notes;

        comboBox3.Text = restore.record3.clientName;
        stopwatches[2].ElapsedTimeSpan = restore.record3.actualTime;
        textBox3.Text = restore.record3.notes;

        comboBox4.Text = restore.record4.clientName;
        stopwatches[3].ElapsedTimeSpan = restore.record4.actualTime;
        textBox4.Text = restore.record4.notes;

        comboBox5.Text = restore.record5.clientName;
        stopwatches[4].ElapsedTimeSpan = restore.record5.actualTime;
        textBox5.Text = restore.record5.notes;
      }
    }

    private bool _running() {
      if(stopwatches[0].IsRunning || stopwatches[1].IsRunning || stopwatches[2].IsRunning || stopwatches[3].IsRunning || stopwatches[4].IsRunning) {
        return true;
      }
      return false;
    }

    private void _save() {
      string padding = "----------------------------------";
      String[] lines = { padding, DateTime.Now.ToString() + " " + comboBox1.Text, padding, "Billable hours: " + timeDisplay1.Text, "Actual hours: " + auxDisplay1.Text, "Notes:", textBox1.Text,
                         padding, comboBox2.Text, padding, "Billable hours: " + timeDisplay2.Text, "Actual hours: " + auxDisplay2.Text, "Notes:", textBox2.Text,
                         padding, comboBox3.Text, padding, "Billable hours: " + timeDisplay3.Text, "Actual hours: " + auxDisplay3.Text, "Notes:", textBox3.Text,
                         padding, comboBox4.Text, padding, "Billable hours: " + timeDisplay4.Text, "Actual hours: " + auxDisplay4.Text, "Notes:", textBox4.Text,
                         padding, comboBox5.Text, padding, "Billable hours: " + timeDisplay5.Text, "Actual hours: " + auxDisplay5.Text, "Notes:", textBox5.Text };
      File.AppendAllLines((_user_loc + "\\Billable_Time" + DateTime.Now.Date.ToString(@"-MMMM_dd_yyyy") + ".txt"), lines);
    }

    private void _backup() {
      count++;
      if (count >= 100) {
        RecordEntry r1 = new RecordEntry() { clientName = comboBox1.Text, billableTime = timeDisplay1.Text, actualTime = stopwatches[0].ElapsedTimeSpan, notes = textBox1.Text };
        RecordEntry r2 = new RecordEntry() { clientName = comboBox2.Text, billableTime = timeDisplay2.Text, actualTime = stopwatches[1].ElapsedTimeSpan, notes = textBox2.Text };
        RecordEntry r3 = new RecordEntry() { clientName = comboBox3.Text, billableTime = timeDisplay3.Text, actualTime = stopwatches[2].ElapsedTimeSpan, notes = textBox3.Text };
        RecordEntry r4 = new RecordEntry() { clientName = comboBox4.Text, billableTime = timeDisplay4.Text, actualTime = stopwatches[3].ElapsedTimeSpan, notes = textBox4.Text };
        RecordEntry r5 = new RecordEntry() { clientName = comboBox5.Text, billableTime = timeDisplay5.Text, actualTime = stopwatches[4].ElapsedTimeSpan, notes = textBox5.Text };

        TimerRecord backupRecord = new TimerRecord() { record1 = r1, record2 = r2, record3 = r3, record4 = r4, record5 = r5 };

        BinarySerialization.WriteToBinaryFile<TimerRecord>(_bak_loc + "\\Billable_Time_Backup.bak", backupRecord);
        count = 0;
      }
    }

    private void saveToolStripMenuItem1_Click(object sender, EventArgs e) {
      _save();
    }

    private void restoreToolStripMenuItem2_Click(object sender, EventArgs e) {
      _restore();
    }

    private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e) {
      toggleTopmost();
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
      About about = new About();
      about.Show();
    }
  }
}
