using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.ComponentModel;

namespace TEST {

    
    [Serializable] public class AKLData : DataListCollection {
        internal DataList<TB_050Item> _TB_050;
        public DataList<TB_050Item> TB_050 {
            get {
                _TB_050 ??= AddToCollection(new DataList<TB_050Item>());
                return _TB_050;
            }
            set {
                AddToCollection(value);
                _TB_050 = value;
            }
        }

        internal DataList<AKL_FEHLERItem> _AKL_FEHLER;
        public DataList<AKL_FEHLERItem> AKL_FEHLER {
            get {
                _AKL_FEHLER ??= AddToCollection(new DataList<AKL_FEHLERItem>());
                return _AKL_FEHLER;
            }
            set {
                AddToCollection(value);
                _AKL_FEHLER = value;
            }
        }

        internal DataList<StockTransferItem> _StockTransfer;
        public DataList<StockTransferItem> StockTransfer {
            get {
                _StockTransfer ??= AddToCollection(new DataList<StockTransferItem>());
                return _StockTransfer;
            }
            set {
                AddToCollection(value);
                _StockTransfer = value;
            }
        }

        internal DataList<BinLocationItem> _BinLocation;
        public DataList<BinLocationItem> BinLocation {
            get {
                _BinLocation ??= AddToCollection(new DataList<BinLocationItem>());
                return _BinLocation;
            }
            set {
                AddToCollection(value);
                _BinLocation = value;
            }
        }

        internal DataList<SAPBINItem> _SAPBIN;
        public DataList<SAPBINItem> SAPBIN {
            get {
                _SAPBIN ??= AddToCollection(new DataList<SAPBINItem>());
                return _SAPBIN;
            }
            set {
                AddToCollection(value);
                _SAPBIN = value;
            }
        }

        internal DataList<BESTAND_VERFItem> _BESTAND_VERF;
        public DataList<BESTAND_VERFItem> BESTAND_VERF {
            get {
                _BESTAND_VERF ??= AddToCollection(new DataList<BESTAND_VERFItem>());
                return _BESTAND_VERF;
            }
            set {
                AddToCollection(value);
                _BESTAND_VERF = value;
            }
        }

        internal DataList<SEQNOsItem> _SEQNOs;
        public DataList<SEQNOsItem> SEQNOs {
            get {
                _SEQNOs ??= AddToCollection(new DataList<SEQNOsItem>());
                return _SEQNOs;
            }
            set {
                AddToCollection(value);
                _SEQNOs = value;
            }
        }

        internal DataList<VISUAL_ASItem> _VISUAL_AS;
        public DataList<VISUAL_ASItem> VISUAL_AS {
            get {
                _VISUAL_AS ??= AddToCollection(new DataList<VISUAL_ASItem>());
                return _VISUAL_AS;
            }
            set {
                AddToCollection(value);
                _VISUAL_AS = value;
            }
        }

        internal DataList<VISUAL_WSItem> _VISUAL_WS;
        public DataList<VISUAL_WSItem> VISUAL_WS {
            get {
                _VISUAL_WS ??= AddToCollection(new DataList<VISUAL_WSItem>());
                return _VISUAL_WS;
            }
            set {
                AddToCollection(value);
                _VISUAL_WS = value;
            }
        }

        internal DataList<VISUAL_LCItem> _VISUAL_LC;
        public DataList<VISUAL_LCItem> VISUAL_LC {
            get {
                _VISUAL_LC ??= AddToCollection(new DataList<VISUAL_LCItem>());
                return _VISUAL_LC;
            }
            set {
                AddToCollection(value);
                _VISUAL_LC = value;
            }
        }

        internal DataList<AKL_FEHLER_RECItem> _AKL_FEHLER_REC;
        public DataList<AKL_FEHLER_RECItem> AKL_FEHLER_REC {
            get {
                _AKL_FEHLER_REC ??= AddToCollection(new DataList<AKL_FEHLER_RECItem>());
                return _AKL_FEHLER_REC;
            }
            set {
                AddToCollection(value);
                _AKL_FEHLER_REC = value;
            }
        }

        internal DataList<HOST_RECItem> _HOST_REC;
        public DataList<HOST_RECItem> HOST_REC {
            get {
                _HOST_REC ??= AddToCollection(new DataList<HOST_RECItem>());
                return _HOST_REC;
            }
            set {
                AddToCollection(value);
                _HOST_REC = value;
            }
        }

        internal DataList<HOST_SENDItem> _HOST_SEND;
        public DataList<HOST_SENDItem> HOST_SEND {
            get {
                _HOST_SEND ??= AddToCollection(new DataList<HOST_SENDItem>());
                return _HOST_SEND;
            }
            set {
                AddToCollection(value);
                _HOST_SEND = value;
            }
        }

        internal DataList<COUNTItem> _COUNT;
        public DataList<COUNTItem> COUNT {
            get {
                _COUNT ??= AddToCollection(new DataList<COUNTItem>());
                return _COUNT;
            }
            set {
                AddToCollection(value);
                _COUNT = value;
            }
        }

        internal DataList<SEQ_NOsItem> _SEQ_NOs;
        public DataList<SEQ_NOsItem> SEQ_NOs {
            get {
                _SEQ_NOs ??= AddToCollection(new DataList<SEQ_NOsItem>());
                return _SEQ_NOs;
            }
            set {
                AddToCollection(value);
                _SEQ_NOs = value;
            }
        }

        internal DataList<AKLKartonCodesItem> _AKLKartonCodes;
        public DataList<AKLKartonCodesItem> AKLKartonCodes {
            get {
                _AKLKartonCodes ??= AddToCollection(new DataList<AKLKartonCodesItem>());
                return _AKLKartonCodes;
            }
            set {
                AddToCollection(value);
                _AKLKartonCodes = value;
            }
        }



        [Table("@TB_050")]
        [Serializable] public class TB_050Item : DataItem, INotifyPropertyChanged {
            [Column("Code"), PrimaryKey] public String? Code { get; set; }
            [Column("Name"), PrimaryKey] public String? Name { get; set; }
            [Column("U_LogInstanc")] public Int32? U_LogInstanc { get; set; }
        }

        [Table("AKL_FEHLER")]
        [Serializable] public class AKL_FEHLERItem : DataItem, INotifyPropertyChanged {
            [Column("Code")] public String? Code { get; set; }
            [Column("Name")] public String? Name { get; set; }
            [Column("U_Description")] public String? U_Description { get; set; }
            [Column("U_PROBLEM_NO")] public Int32? U_PROBLEM_NO { get; set; }
            [Column("U_PROBLEM_INFO")] public String? U_PROBLEM_INFO { get; set; }
            [Column("checkboxCancel")] public Boolean? checkboxCancel { get; set; }
            [Column("checkbox")] public Boolean? checkbox { get; set; }
            [Column("U_TYP")] public String? U_TYP { get; set; }
            [Column("U_LAST_TRY")] public DateTime? U_LAST_TRY { get; set; }
        }

        [Table("StockTransfer")]
        [Serializable] public class StockTransferItem : DataItem, INotifyPropertyChanged {
            [Column("ItemCode")] public String? ItemCode { get; set; }
            [Column("PickedQuantity")] public Int32? PickedQuantity { get; set; }
            [Column("OWTQDocEntry")] public Int32? OWTQDocEntry { get; set; }
            [Column("OWTQDocNum")] public Int32? OWTQDocNum { get; set; }
            [Column("OWTQLineNum")] public Int32? OWTQLineNum { get; set; }
            [Column("FromWhsCode")] public String? FromWhsCode { get; set; }
            [Column("WhsCode")] public String? WhsCode { get; set; }
            [Column("BinAbsEntry")] public Int32? BinAbsEntry { get; set; }
            [Column("NVE")] public String? NVE { get; set; }
            [Column("ToBinCode")] public String? ToBinCode { get; set; }
            [Column("ORDRDocEntry")] public Int32? ORDRDocEntry { get; set; }
            [Column("ORDRDocNum")] public Int32? ORDRDocNum { get; set; }
        }

        [Table("BinLocation")]
        [Serializable] public class BinLocationItem : DataItem, INotifyPropertyChanged {
            [Column("WhsCode")] public String? WhsCode { get; set; }
            [Column("SL1Code")] public String? SL1Code { get; set; }
        }

        [Table("SAPBIN")]
        [Serializable] public class SAPBINItem : DataItem, INotifyPropertyChanged {
            [Column("ItemCode")] public String? ItemCode { get; set; }
            [Column("Quantity")] public Decimal? Quantity { get; set; }
            [Column("OnHandQty")] public Decimal? OnHandQty { get; set; }
        }

        [Table("BESTAND_VERF")]
        [Serializable] public class BESTAND_VERFItem : DataItem, INotifyPropertyChanged {
            [Column("ItemCode")] public String? ItemCode { get; set; }
            [Column("Quantity")] public Decimal? Quantity { get; set; }
            [Column("AKLMeldung")] public Decimal? AKLMeldung { get; set; }
            [Column("MENGE_VERF")] public Decimal? MENGE_VERF { get; set; }
            [Column("Picked_Quantity")] public Decimal? Picked_Quantity { get; set; }
        }

        [Table("SEQNOs")]
        [Serializable] public class SEQNOsItem : DataItem, INotifyPropertyChanged {
            [Column("SEQ_NO")] public Int32? SEQ_NO { get; set; }
        }

        [Table("VISUAL_AS")]
        [Serializable] public class VISUAL_ASItem : DataItem, INotifyPropertyChanged {
            [Column("TIMESTAMP_LOG")] public DateTime? TIMESTAMP_LOG { get; set; }
            [Column("TIMESTAMP_INSERTED")] public DateTime? TIMESTAMP_INSERTED { get; set; }
            [Column("SEQ_NO")] public Int32? SEQ_NO { get; set; }
            [Column("SOURCE")] public String? SOURCE { get; set; }
            [Column("TARGET")] public String? TARGET { get; set; }
            [Column("TIMESTAMP_LAST_UPDATE")] public DateTime? TIMESTAMP_LAST_UPDATE { get; set; }
            [Column("STATE")] public Int32? STATE { get; set; }
            [Column("RECORD_TYPE")] public String? RECORD_TYPE { get; set; }
            [Column("RECORD_MODE")] public String? RECORD_MODE { get; set; }
            [Column("VERSION")] public String? VERSION { get; set; }
            [Column("MESSAGE")] public String? MESSAGE { get; set; }
            [Column("PROBLEM_NO")] public Int32? PROBLEM_NO { get; set; }
            [Column("PROBLEM_INFO")] public String? PROBLEM_INFO { get; set; }
            [Column("PARAMETER_ID")] public String? PARAMETER_ID { get; set; }
            [Column("CHANNEL_ID")] public String? CHANNEL_ID { get; set; }
            [Column("SEND_ALERT")] public String? SEND_ALERT { get; set; }
            [Column("CHECK_COUNT")] public Int32? CHECK_COUNT { get; set; }
            [Column("AK_STATUS")] public String? AK_STATUS { get; set; }
            [Column("AKL_FEHLER")] public String? AKL_FEHLER { get; set; }
            [Column("KL_AbsEntry")] public String? KL_AbsEntry { get; set; }
            [Column("PickEntry")] public String? PickEntry { get; set; }
            [Column("ItemCode")] public String? ItemCode { get; set; }
            [Column("PickQty")] public String? PickQty { get; set; }
            [Column("AKL_KartonCode")] public String? AKL_KartonCode { get; set; }
            [Column("KP")] public String? KP { get; set; }
        }

        [Table("VISUAL_WS")]
        [Serializable] public class VISUAL_WSItem : DataItem, INotifyPropertyChanged {
            [Column("TIMESTAMP_LOG")] public DateTime? TIMESTAMP_LOG { get; set; }
            [Column("TIMESTAMP_INSERTED")] public DateTime? TIMESTAMP_INSERTED { get; set; }
            [Column("SEQ_NO")] public Int32? SEQ_NO { get; set; }
            [Column("SOURCE")] public String? SOURCE { get; set; }
            [Column("TARGET")] public String? TARGET { get; set; }
            [Column("TIMESTAMP_LAST_UPDATE")] public DateTime? TIMESTAMP_LAST_UPDATE { get; set; }
            [Column("STATE")] public Int32? STATE { get; set; }
            [Column("RECORD_TYPE")] public String? RECORD_TYPE { get; set; }
            [Column("RECORD_MODE")] public String? RECORD_MODE { get; set; }
            [Column("VERSION")] public String? VERSION { get; set; }
            [Column("MESSAGE")] public String? MESSAGE { get; set; }
            [Column("PROBLEM_NO")] public Int32? PROBLEM_NO { get; set; }
            [Column("PROBLEM_INFO")] public String? PROBLEM_INFO { get; set; }
            [Column("PARAMETER_ID")] public String? PARAMETER_ID { get; set; }
            [Column("CHANNEL_ID")] public String? CHANNEL_ID { get; set; }
            [Column("SEND_ALERT")] public String? SEND_ALERT { get; set; }
            [Column("CHECK_COUNT")] public Int32? CHECK_COUNT { get; set; }
            [Column("WR_STATUS")] public String? WR_STATUS { get; set; }
            [Column("AKL_FEHLER")] public String? AKL_FEHLER { get; set; }
            [Column("DOCENTRY")] public String? DOCENTRY { get; set; }
            [Column("DOCNUM")] public String? DOCNUM { get; set; }
            [Column("LINENUM")] public String? LINENUM { get; set; }
            [Column("NVENR")] public String? NVENR { get; set; }
            [Column("RSTS")] public String? RSTS { get; set; }
            [Column("DAT")] public String? DAT { get; set; }
            [Column("UZT")] public String? UZT { get; set; }
            [Column("ZIEL")] public String? ZIEL { get; set; }
            [Column("KARTONID")] public String? KARTONID { get; set; }
            [Column("MENGE")] public String? MENGE { get; set; }
            [Column("ITEMCODE")] public String? ITEMCODE { get; set; }
        }

        [Table("VISUAL_LC")]
        [Serializable] public class VISUAL_LCItem : DataItem, INotifyPropertyChanged {
            [Column("TIMESTAMP_LOG")] public DateTime? TIMESTAMP_LOG { get; set; }
            [Column("TIMESTAMP_INSERTED")] public DateTime? TIMESTAMP_INSERTED { get; set; }
            [Column("SEQ_NO")] public Int32? SEQ_NO { get; set; }
            [Column("SOURCE")] public String? SOURCE { get; set; }
            [Column("TARGET")] public String? TARGET { get; set; }
            [Column("TIMESTAMP_LAST_UPDATE")] public DateTime? TIMESTAMP_LAST_UPDATE { get; set; }
            [Column("TIMESTAMP_MELDUNG")] public DateTime? TIMESTAMP_MELDUNG { get; set; }
            [Column("STATE")] public Int32? STATE { get; set; }
            [Column("RECORD_TYPE")] public String? RECORD_TYPE { get; set; }
            [Column("RECORD_MODE")] public String? RECORD_MODE { get; set; }
            [Column("VERSION")] public String? VERSION { get; set; }
            [Column("MESSAGE")] public String? MESSAGE { get; set; }
            [Column("PROBLEM_NO")] public Int32? PROBLEM_NO { get; set; }
            [Column("PROBLEM_INFO")] public String? PROBLEM_INFO { get; set; }
            [Column("PARAMETER_ID")] public String? PARAMETER_ID { get; set; }
            [Column("CHANNEL_ID")] public String? CHANNEL_ID { get; set; }
            [Column("SEND_ALERT")] public String? SEND_ALERT { get; set; }
            [Column("CHECK_COUNT")] public Int32? CHECK_COUNT { get; set; }
            [Column("AKL_FEHLER")] public String? AKL_FEHLER { get; set; }
            [Column("ABSENTRY")] public String? ABSENTRY { get; set; }
            [Column("ITEMCODE")] public String? ITEMCODE { get; set; }
            [Column("TYP")] public String? TYP { get; set; }
            [Column("QTTY")] public String? QTTY { get; set; }
            [Column("BESTAND")] public String? BESTAND { get; set; }
            [Column("STDAT")] public String? STDAT { get; set; }
            [Column("STUZT")] public String? STUZT { get; set; }
        }

        [Table("@AKL_FEHLER_REC")]
        [Serializable] public class AKL_FEHLER_RECItem : DataItem, INotifyPropertyChanged {
            [Column("Code"), PrimaryKey] public String? Code { get; set; }
            [Column("Name"), PrimaryKey] public String? Name { get; set; }
            [Column("U_Description")] public String? U_Description { get; set; }
            [Column("U_PROBLEM_NO")] public Int32? U_PROBLEM_NO { get; set; }
            [Column("U_PROBLEM_INFO")] public String? U_PROBLEM_INFO { get; set; }
            [Column("checkboxCancel")] public Boolean? checkboxCancel { get; set; }
            [Column("checkbox")] public Boolean? checkbox { get; set; }
            [Column("U_TYP")] public String? U_TYP { get; set; }
            [Column("U_LAST_TRY")] public DateTime? U_LAST_TRY { get; set; }
        }

        [Table("HOST_REC")]
        [Serializable] public class HOST_RECItem : DataItem, INotifyPropertyChanged {
            [Column("TIMESTAMP_INSERTED")] public DateTime? TIMESTAMP_INSERTED { get; set; }
            [Column("SEQ_NO")] public Int32? SEQ_NO { get; set; }
            [Column("SOURCE")] public String? SOURCE { get; set; }
            [Column("TARGET")] public String? TARGET { get; set; }
            [Column("TIMESTAMP_LAST_UPDATE")] public DateTime? TIMESTAMP_LAST_UPDATE { get; set; }
            [Column("STATE")] public Int32? STATE { get; set; }
            [Column("RECORD_TYPE")] public String? RECORD_TYPE { get; set; }
            [Column("RECORD_MODE")] public String? RECORD_MODE { get; set; }
            [Column("VERSION")] public String? VERSION { get; set; }
            [Column("MESSAGE")] public String? MESSAGE { get; set; }
            [Column("PROBLEM_NO")] public Int32? PROBLEM_NO { get; set; }
            [Column("PROBLEM_INFO")] public String? PROBLEM_INFO { get; set; }
            [Column("PARAMETER_ID")] public String? PARAMETER_ID { get; set; }
            [Column("CHANNEL_ID")] public String? CHANNEL_ID { get; set; }
            [Column("SEND_ALERT")] public String? SEND_ALERT { get; set; }
            [Column("CHECK_COUNT")] public Int32? CHECK_COUNT { get; set; }
            [Column("EXTRA")] public String? EXTRA { get; set; }
        }

        [Table("HOST_SEND")]
        [Serializable] public class HOST_SENDItem : DataItem, INotifyPropertyChanged {
            [Column("TIMESTAMP_INSERTED")] public DateTime? TIMESTAMP_INSERTED { get; set; }
            [Column("SEQ_NO")] public Int32? SEQ_NO { get; set; }
            [Column("SOURCE")] public String? SOURCE { get; set; }
            [Column("TARGET")] public String? TARGET { get; set; }
            [Column("TIMESTAMP_LAST_UPDATE")] public DateTime? TIMESTAMP_LAST_UPDATE { get; set; }
            [Column("STATE")] public Int32? STATE { get; set; }
            [Column("RECORD_TYPE")] public String? RECORD_TYPE { get; set; }
            [Column("RECORD_MODE")] public String? RECORD_MODE { get; set; }
            [Column("VERSION")] public String? VERSION { get; set; }
            [Column("MESSAGE")] public String? MESSAGE { get; set; }
            [Column("PROBLEM_NO")] public Int32? PROBLEM_NO { get; set; }
            [Column("PROBLEM_INFO")] public String? PROBLEM_INFO { get; set; }
            [Column("PARAMETER_ID")] public String? PARAMETER_ID { get; set; }
            [Column("CHANNEL_ID")] public String? CHANNEL_ID { get; set; }
            [Column("SEND_ALERT")] public String? SEND_ALERT { get; set; }
            [Column("CHECK_COUNT")] public Int32? CHECK_COUNT { get; set; }
        }

        [Table("COUNT")]
        [Serializable] public class COUNTItem : DataItem, INotifyPropertyChanged {
            [Column("COUNT")] public Int32? COUNT { get; set; }
        }

        [Table("SEQ_NOs")]
        [Serializable] public class SEQ_NOsItem : DataItem, INotifyPropertyChanged {
            [Column("SEQ_NO")] public Int32? SEQ_NO { get; set; }
        }

        [Table("AKLKartonCodes")]
        [Serializable] public class AKLKartonCodesItem : DataItem, INotifyPropertyChanged {
            [Column("AKL_KARTONCODE")] public String? AKL_KARTONCODE { get; set; }
            [Column("KL_AbsEntry")] public String? KL_AbsEntry { get; set; }
        }

    }
}