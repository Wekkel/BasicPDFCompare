# BasicPDFCompare
Basic compare PDF (text) and provide output as PDF file

What does it do?
Compare 2 PDF files and save the result as a PDF file. The compare is limited to flat text only.

The executable takes 3 arguments:
-full_path_to_sourcefile_1
-full_path_to_sourcefile_2
-full_path_to_destinationfile

Example:

>BasicPDFCompare.exe "c:\test\source1.pdf" "c:\test\source2.pdf" "c:\test\result.pdf"

Why?
I needed a straight forward way to (Powershell) batch compare (text only) a couple of PDF files and did not find a suitable solution online.

How
The source PDF files are read for text content with iTextSharp. 

The texts found in both sourcefile PDFs is compared with GoogleDiffMatchPatch (default settings). 

The result is converted into HTML to get a color coded result. Insertions marked green and deletions in red with strikethrough.

The HTML is cleaned on a couple of points so that conversion into PDF does not result in loss of the color scheme. Basically, (i) a <head> is added to the HTML with CSS style elements for insertions (green) and deletions (red) and (ii) the existing < del > and < ins > tags are cleaned from style content.

Subsequently, the HTML is converted into and saved as a PDF file with HTML Renderer and PdfSharp (could not use iTextSharp because the color coding gets lost along the way).

License
The code is licensed in accordance with the  mandatory requirements of the licenses applicable to the software referenced in this code (i.e., iTextSharp, PdfSharp, HtmlRenderer and GoogleDiffMatchPatch).

Desired options
The command line options could be updated for robustness and flexibility:
-more input checks, like: does source file exist, does output filepath folder exists, batch options (select entire folder)
-default options (e.g., write output file to application path if not specified at command line)
-choose different color scheme for insertions and deletions
-set specific compare settings
-(in batch mode) option to sort the output PDF files and combine them in a single PDF

Overall, the code could use more robustness (check on null values, informative error reports, etc).

I do not plan to include these features myself.
