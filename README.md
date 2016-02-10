##Features
### 1. MainBit.Layouts
Provides a html extension to render an element with specific display type.
### 2. MainBit.Layouts.Tokens
Provides an element token provider that enables elements to be rendered with specific display type using a token.
### 3. MainBit.Layouts.Alternates
####Adds alternates for some elements
* `Elements_{ElementName}_ContentType_{Content Type witch element is added to}`. Example ~/Views/Elements/Grid.ContentType-BlogPost.cshtml)
* `Elements_MediaItem_{Media Item Display Type}`. Example ~/Views/Elements/MediaItem.Gallery.cshtml
####Allow to specify custom display type (and alternate correspondingly)
* You can specify display type for custom element in LayoutEditor and create alternate based on that display type. For specify display type you should type in css classes field `DisplayType:TypeName ClassesIfNeeded`. Example `DisplayType:Sitemap b-nice-sitemap` or `DisplayType:Sitemap`. And then you should create alternate `Elements_{ElementName}_{DisplayType}`. Example `~/Views/Elements/Elements.Menu.Sitemap.cshtml`.
* When you configure DisplayType for menu element that special alternates will be added to menu, menuItem and menuItemLink shape. Example: `~/Views/Menu.Sitemap.cshtml`, `~/Views/MenuItem.Sitemap.cshtml`.

### 4. MainBit.Layouts.Relations
Provides a content part that displays related layout parts (Content items with layout parts witch specific content item is added to). Example: enable feature, then create MyVideo content type with ContentLayoutMapPart content part, then create MyVideo content item, then include MyVideo content item to three BlogPost content item by ContentItem Element, then you can see related content items (three blog post items) on MyVideo content item page.
### 5. MainBit.Layouts.Compounds
Provides a way to grouping elements and leater use this group as one element (with editable subelements)
