#if !NO_UI_WEB_BROWSER
//
// This file manually written from cef/include/internal/cef_types.h.
// C API name: cef_xml_node_type_t.
//
namespace Internal.Xilium.CefGlue
{
    /// <summary>
    /// XML node types.
    /// </summary>
    public enum CefXmlNodeType
    {
        Unsupported = 0,
        ProcessingInstruction,
        DocumentType,
        ElementStart,
        ElementEnd,
        Attribute,
        Text,
        CData,
        EntityReference,
        WhiteSpace,
        Comment,
    }
}

#endif