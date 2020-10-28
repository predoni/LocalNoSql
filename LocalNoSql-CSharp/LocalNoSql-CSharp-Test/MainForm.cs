using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LocalNoSql_CSharp;
using System.IO;

namespace LocalNoSql_CSharp_Test
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            TestFileReader();
        }

        public void TestFileReader()
        {
            //LocalNoSql_CSharp.Common.LineReader.Example();
        }

        public void TestJson()
        {
            //Newtonsoft.Json.
            //Newtonsoft.Json.Bson.BsonDataReader bsonDataReader = new Newtonsoft.Json.Bson.BsonDataReader();

            string schemaJson = @"{
  'description': 'A person',
  'type': 'object',
  'properties':
  {
    'name': {'type':'string'},
    'hobbies': {
      'type': 'array',
      'items': {'type':'string'}
    }
  }
}";

            Newtonsoft.Json.Schema.JSchema schema = Newtonsoft.Json.Schema.JSchema.Parse(schemaJson);

            string jsonStr = @"{
  'name': 'James',
  'hobbies': ['.NET', 'Blogg


ing', 'Reading', 'Xbox', 'LOLCATS']
}";

            Newtonsoft.Json.Linq.JObject person = Newtonsoft.Json.Linq.JObject.Parse(jsonStr);

            bool valid = person.IsValid(schema);
            System.Diagnostics.Debug.WriteLine("is valid: " + valid.ToString());

            //string newJson = person.ToString(Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonConverter[] { });
            string newJson = person.ToString(Newtonsoft.Json.Formatting.None, null);
            System.Diagnostics.Debug.WriteLine("=======================================");
            System.Diagnostics.Debug.WriteLine(jsonStr);
            System.Diagnostics.Debug.WriteLine("=======================================");
            System.Diagnostics.Debug.WriteLine(newJson);
            System.Diagnostics.Debug.WriteLine("=======================================");
        }
    }
}
