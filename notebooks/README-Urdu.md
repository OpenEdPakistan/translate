[انگریزی میں پڑھیے](README.md)
<br />
<h1 align="right">انگریزی عبارت کا اردو میں ترجمہ کرنے کے لئے مائیکروسافٹ کاگنوٹو سروسز ٹرانسلیٹر اے پی آئی اور پائتھن نوٹ بک کا استعمال</h1>
<h2 align="right">کیوں؟</h2>
<p align="right">
اردو میں ترجمہ: اوپن پاکستان ایجوکیشن نیٹ ورک کورسز میں اردو آڈیو بیانیہ کے لئے اردو میں متن سے آواز کا استعمال کیا جاتا ہے۔ اس اردو عبارت کو تخلیق کرنے کے لئے مائیکرو سافٹ کاگنٹو سروسز ٹرانسلٹر اے پی آئی سے پہلے انگریزی متن کا اردو میں ترجمہ کیا جاتا ہے۔ 
</p>
<h2 align="right">کیا؟</h2>
<p align="right">
جپیٹر نوٹ بک سے مترجم اے پی آئی کو پکارنا: ذیل میں دکھایا گیا نقطہ نظر کچھ معمولی تبدیلیوں کے ساتھ پائتھن کوڈ (اصل میں گٹ حب کے ذیلی لنک پر شائع کیا گیا) کا استعمال کرتے ہوئےمترجم اے پی آئی پکارنے پر مشتمل ہے۔ 

https://github.com/MicrosoftTranslator/Text-Translation-API-V3-Python/blob/master/Translate.py
</p>
<a target="_blank" rel="noopener noreferrer" href="../files/Translate-Python.png"><img src="../files/Translate-Python.png" alt="Translate Workflow" style="max-width:100%;"></a>
<h2 align="right">کیسے؟</h2>
<p align="right">
ذیلی اقدامات یہ فرض کرتے ہیں کہ ایزر پورٹل کا استعمال کرتے ہوئے آپ کو مائیکروسافٹ کلاؤڈ کی ایک عدد عوامی سبسکرپشن تک رسائی حاصل ہے۔ مندرجہ ذیل حالتی شکل ایک اعلی سطح پر عمل کو ظاہر کرتی ہے۔
<br />
<a target="_blank" rel="noopener noreferrer" href="../files/Translate-STD.png"><img src="../files/Translate-STD.png" alt="State Diagram" style="max-width:100%;"></a>
 <br />
 مذکورہ شکل میں نشان دہی کیے گئے 3 حصے پروگرام میں عنوانات کے مطابق ہیں۔
 <br />
ایزر پورٹل میں لاگ ان کیجئے اور مترجم اے پی آئی تشکیل دیجئے (ذیل میں لنک پر دیے گئے مراحل کی پیروی کیجئے)۔
 <br />
 https://docs.microsoft.com/en-us/azure/cognitive-services/translator/translator-how-to-signup
  <br />
'نوٹ بک' فولڈر سے نوٹ بک فائل ڈاؤن لوڈ کیجئے اور اسے جپیٹر یا وژوئل اسٹوڈیو کوڈ میں دیکھئے۔ (ذیل کے دو مضامین پڑھیے)
کوڈ فائل میں تشکیل کردہ اے پی آئی کی سبسکرپشن شناخت کی وضاحت کیجئے۔
 <br />
پائتھن کوڈ فائل امیں انگریزی کے بیانات JSON میں بیان کیجئے۔
اردو عبارت کو یونیکوڈ عنٹیٹیز کی حیثیت سے دیکھنے کیلئے نوٹ بک کے ہر سیل کو چلائیں۔
 <br />
ذیل میں لنک پر دوسرا ٹیکسٹ ایریا استعمال کیجئے اور عنٹیٹیز کو تبدیل کرکے پیدا شدہ اردو ترجمہ دیکھیے۔
 <br />
 https://www.online-toolz.com/tools/text-unicode-entities-convertor.php
 <br />
</p>
<h3 align="right">کوڈ</h3>
<h4 align="right">١۔ ایزر پورٹل سے سبسکرپشن شناخت حاصل کیجئے</h4>
<pre>
import os, requests, uuid, json

subscription_key = '[Insert Key here]'
endpoint = 'https://api.cognitive.microsofttranslator.com/translate?api-version=3.0'
params = '&from=en&to=ur'
constructed_url = endpoint + params

headers = {
    'Ocp-Apim-Subscription-Key': subscription_key,
    'Ocp-Apim-Subscription-Region' : 'southcentralus',
    'Content-type': 'application/json',
    'X-ClientTraceId': str(uuid.uuid4())
}
</pre>
<br />
<h4 align="right">٢۔ ترجمہ اے پی آئی کال کیجئے</h4>
<pre>
body = [{ 'text' : 'Welcome to Open Pakistan Education Network.' }, { 'text' : 'Learn something new or update your existing knowledge.' }, { 'text' : 'All content is available in Urdu and English.' }]
request = requests.post(constructed_url, headers=headers, json=body)
response = request.json()
</pre>
<br />
<h4 align="right">٣۔ صارف کو دکھانے کے لیے اے پی آئی کے نتائج کی اشاعت کیجئے</h4>
<pre>
print(json.dumps(response, sort_keys=True, indent=4, separators=(',', ': ')))
</pre>
<br />
