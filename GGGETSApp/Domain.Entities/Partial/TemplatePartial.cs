using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    ///<summary>
    /// 打印纸张
    ///</summary>
    public enum PrintPage
    {
        Letter=0, LetterSmall, Tabloid, Ledger, Legal, Statement, Executive,
        A3, A4, A4Small, A5, B4, B5, Folio, Quarto, qr10X14, qr11X17, Note,
        Env9, Env10, Env11, Env12, Env14, Sheet, DSheet, Esheet

    }
    public partial class Template
    {
    }
}
