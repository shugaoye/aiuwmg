using System;
using Xamarin.Forms;
using CommonMark;
using CommonMark.Syntax;

using PassXYZ.Utils;

namespace AIUWMG.Views
{
    /// <summary>
    /// A View that presents Markdown content.
    /// </summary>
    public class MarkdownView : WebView
    {
        private readonly string _baseUrl;

		public MarkdownView() : this(LinkRenderingOption.Underline)
		{

		}

		/// <summary>
		/// Creates a new MarkdownView
		/// </summary>
		/// <param name="linksOption">Tells the view how to render links.</param>
		public MarkdownView(LinkRenderingOption linksOption)
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

        /// <summary>
        /// Backing store for the MarkdownView.Stylesheet property
        /// </summary>
        public static readonly BindableProperty StylesheetProperty =
            BindableProperty.Create<MarkdownView, string>(
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
                SetStylesheet();
            }
        }

        /// <summary>
        /// Backing storage for the MarkdownView.Markdown property
        /// </summary>
        public static readonly BindableProperty MarkdownProperty =
            BindableProperty.Create<MarkdownView, string>(
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
                SetWebViewSourceFromMarkdown();
            }
        }

        private void SetWebViewSourceFromMarkdown()
        {
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

            // SetStylesheet();
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
    }
    // Add new class here
}

