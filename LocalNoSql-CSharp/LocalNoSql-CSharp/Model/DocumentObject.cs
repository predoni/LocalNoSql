using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.Model
{
    public class DocumentObject<T> : Object
    {
        #region Properties
        public T Id { get; set; }
        #endregion

        #region Constructors and destructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DocumentObject() : base() { }

        /// <summary>
        /// Constructor with initialization.
        /// </summary>
        /// <param name="id">The value for parameter "Id"</param>
        public DocumentObject(T id) : base()
        {
            this.Id = id;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="documentObject">The object to copy.</param>
        public DocumentObject(DocumentObject<T> documentObject) : base()
        {
            this.Id = documentObject.Id;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Retruns the type of parameter "Id".
        /// </summary>
        /// <returns></returns>
        public Type GetFirstPosibleTypeOfId()
        {
            
            if (this.IsDateTime()) return typeof(DateTime);
            if (this.IsBoolean()) return typeof(bool);
            if (this.IsChar()) return typeof(char);
            if (this.IsByte()) return typeof(byte);
            if (this.IsSByte()) return typeof(sbyte);
            if (this.IsInt16()) return typeof(Int16);
            if (this.IsUInt16()) return typeof(UInt16);
            if (this.IsInt32()) return typeof(Int32);
            if (this.IsUInt32()) return typeof(UInt32);
            if (this.IsInt64()) return typeof(Int64);
            if (this.IsUInt64()) return typeof(UInt64);
            if (this.IsFloat()) return typeof(float);
            if (this.IsDouble()) return typeof(double);
            if (this.IsDecimal()) return typeof(decimal);
            if (this.IsString()) return typeof(string);

            return null;
        }

        /// <summary>
        /// Verifies if "Id" property is of type: Boolean
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsBoolean()
        {
            try
            {
                _ = Convert.ToBoolean(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: Byte
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsByte()
        {
            try
            {
                _ = Convert.ToByte(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: SByte
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsSByte()
        {
            try
            {
                _ = Convert.ToSByte(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: Char
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsChar()
        {
            try
            {
                _ = Convert.ToChar(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: DateTime
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsDateTime()
        {
            try
            {
                _ = Convert.ToDateTime(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: Decimal
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsDecimal()
        {
            try
            {
                _ = Convert.ToDecimal(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: Double
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsDouble()
        {
            try
            {
                _ = Convert.ToDouble(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: Int16
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsInt16()
        {
            try
            {
                _ = Convert.ToInt16(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: UInt16
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsUInt16()
        {
            try
            {
                _ = Convert.ToUInt16(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: Int32
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsInt32()
        {
            try
            {
                _ = Convert.ToInt32(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: UInt32
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsUInt32()
        {
            try
            {
                _ = Convert.ToUInt32(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: Int64
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsInt64()
        {
            try
            {
                _ = Convert.ToInt64(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: UInt64
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsUInt64()
        {
            try
            {
                _ = Convert.ToUInt64(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: float
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsFloat()
        {
            try
            {
                _ = Convert.ToSingle(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if "Id" property is of type: String
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public bool IsString()
        {
            try
            {
                _ = Convert.ToString(this.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// It is an "override" and return the string value of "Id" parameter.
        /// </summary>
        /// <returns>true is the correct type, false otherwise.</returns>
        public override string ToString()
        {
            return this.Id.ToString();
        }
        #endregion
    }
}
