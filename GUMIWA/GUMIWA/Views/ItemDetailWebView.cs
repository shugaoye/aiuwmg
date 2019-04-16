using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

using Xamarin.Forms;

#if !COMMONMARK_JS
using CommonMark;
using CommonMark.Syntax;
#endif // COMMONMARK_JS

using PassXYZ.Utils;

namespace GUMIWA.Views
{
    /// <summary>
    /// A View that presents Item details in WebView.
    /// </summary>
    public class ItemDetailWebView : WebView
    {
#if !COMMONMARK_JS
        private readonly string _baseUrl;

		public ItemDetailWebView() : this(LinkRenderingOption.Underline)
		{

		}

		/// <summary>
		/// Creates a new MarkdownView
		/// </summary>
		/// <param name="linksOption">Tells the view how to render links.</param>
		public ItemDetailWebView(LinkRenderingOption linksOption)
        {
            var baseUrlResolver = DependencyService.Get<IShare>();
            if (baseUrlResolver != null)
                _baseUrl = baseUrlResolver.BaseUrl;

            if (linksOption == LinkRenderingOption.Underline)
                CommonMarkSettings.Default.OutputDelegate =
                    (doc, output, settings) =>
                        new UnderlineLinksHtmlFormatter(output, settings).WriteDocument(doc);

            if (linksOption == LinkRenderingOption.None)
                CommonMarkSettings.Default.OutputDelegate =
                    (doc, output, settings) =>
                        new NoneLinksHtmlFormatter(output, settings).WriteDocument(doc);

        }
#endif // COMMONMARK_JS

        /// <summary>
        /// Backing store for the MarkdownView.Stylesheet property
        /// </summary>
        public static readonly BindableProperty StylesheetProperty =
            BindableProperty.Create<ItemDetailWebView, string>(
                p => p.Stylesheet, "");

        /// <summary>
        /// Gets or sets the stylesheet that will be applied to the document
        /// </summary>
        public string Stylesheet
        {
            get { return (String)GetValue(StylesheetProperty); }
            set
            {
                SetValue(StylesheetProperty, value);
#if !COMMONMARK_JS
                SetStylesheet();
#endif // COMMONMARK_JS
            }
        }

        /// <summary>
        /// Backing storage for the MarkdownView.Markdown property
        /// </summary>
        public static readonly BindableProperty MarkdownProperty =
            BindableProperty.Create<ItemDetailWebView, string>(
                p => p.Markdown, "");

        /// <summary>
        /// The markdown content
        /// </summary>
        public string Markdown
        {
            get { return (String)GetValue(MarkdownProperty); }
            set
            {
                SetValue(MarkdownProperty, value);
#if !COMMONMARK_JS
                SetWebViewSourceFromMarkdown();
#endif // COMMONMARK_JS
            }
        }

        private string jsonData;

        /// <summary>
        /// Key/Value pairs as JSON data
        /// </summary>
        public string JsonData
        {
            get { return jsonData; }
            set { jsonData = value; Debug.WriteLine($"set JSON data: {jsonData}"); }
        }

#if !COMMONMARK_JS
        private void SetWebViewSourceFromMarkdown()
        {
            /*
            string head = @"
<head>
    <meta name='viewport' content='width=device-width, initial-scale=1.0, user-scalable=no'>
    <link id='_ss' rel='stylesheet' type='text/css' href ='markdown.css' >
    <script type='text/javascript' defer src='markdown.js'></script>
</head>";

            var body = @"
<body>" +
    CommonMarkConverter.Convert(Markdown) +
"</body>";

            Source = new HtmlWebViewSource { Html = "<html>" + head + body + "</html>", BaseUrl = _baseUrl };
            */
            // SetStylesheet();

            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("GUMIWA.Resources.index.html");
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                var html_text = reader.ReadToEnd();
                string pattern = "<!--GUMIWA_Notes-->";
                string result = Regex.Replace(html_text, pattern, CommonMarkConverter.Convert(Markdown));
                Source = new HtmlWebViewSource { Html = result, BaseUrl = _baseUrl };
                Debug.WriteLine($"{result}");
            }

        }

        private void SetStylesheet()
        {
            if (!String.IsNullOrEmpty(Stylesheet))
            {
                Eval("_sw(\"" + Stylesheet + "\")");
            }
        }
    }

    public enum LinkRenderingOption
    {
        Underline,
        Default,
        None
    }

    /// <summary>
    /// A formatter that will ignore all link tags inside a markdown document
    /// </summary>
	internal class NoneLinksHtmlFormatter : CommonMark.Formatters.HtmlFormatter
    {
        public NoneLinksHtmlFormatter(System.IO.TextWriter target, CommonMarkSettings settings)
            : base(target, settings)
        {
        }

        protected override void WriteInline(Inline inline, bool isOpening, bool isClosing, out bool ignoreChildNodes)
        {
            if (inline.Tag == InlineTag.Link
                && !this.RenderPlainTextInlines.Peek())
            {
                ignoreChildNodes = false;

                if (isOpening)
                {
                    Write(inline.LiteralContent);
                }
            }
            else
            {
                base.WriteInline(inline, isOpening, isClosing, out ignoreChildNodes);
            }
        }
    }

    /// <summary>
    /// A formatter that will underline all link tags inside a markdown document
    /// </summary>
	internal class UnderlineLinksHtmlFormatter : CommonMark.Formatters.HtmlFormatter
    {
        public UnderlineLinksHtmlFormatter(System.IO.TextWriter target, CommonMarkSettings settings)
            : base(target, settings)
        {
        }

        protected override void WriteInline(Inline inline, bool isOpening, bool isClosing, out bool ignoreChildNodes)
        {
            if (inline.Tag == InlineTag.Link
                && !this.RenderPlainTextInlines.Peek())
            {
                ignoreChildNodes = false;

                // start and end of each node may be visited separately
                if (isOpening)
                {
                    this.Write("<u>");
                    this.Write(inline.LiteralContent);
                }

                if (isClosing)
                {
                    this.Write("</u>");
                }
            }
            else
            {
                base.WriteInline(inline, isOpening, isClosing, out ignoreChildNodes);
            }
        }
#endif // COMMONMARK_JS
    }
    // Add new class here
}

