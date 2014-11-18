IE-frequent-tab-modifier
========================

Helps you undo clicking the x on a frequent tab on the `about:tabs` page in Internet Explorer 9+.

**Instructions:**
Run the command line tool.
It will identify some URL-Hash pairs that you have placed on the exclude list by clicking the x on the new tab page.
Open regedit.exe and navigate to `HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\TabbedBrowsing\NewTabPage\Exclude`.
If there are URLs you'd like to remove from the exclude list, delete the key with the hash reported by the tool.
Go to that site a few time in IE and it should reappear in the frequent section of `about:tabs`.

Uses the IE History Wrapper from http://www.codeproject.com/Articles/7500/The-Tiny-Wrapper-Class-for-URL-History-Interface-i
