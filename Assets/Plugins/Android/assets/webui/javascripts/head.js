
(function(a){var b=a.documentElement,c=navigator.userAgent.toLowerCase().indexOf("msie")!=-1,d=false,e=[],f={},g={},h=a.createElement("script").async===true||"MozAppearance"in a.documentElement.style||window.opera;var i=window.head_conf&&head_conf.head||"head",j=window[i]=window[i]||function(){j.ready.apply(null,arguments)};var k=0,l=1,m=2,n=3;h?j.js=function(){var a=arguments,b=a[a.length-1],c=[];r(b)||(b=null),q(a,function(d,e){d!=b&&(d=p(d),c.push(d),v(d,b&&e==a.length-2?function(){s(c)&&b()}:null))});return j}:j.js=function(){var a=arguments,b=[].slice.call(a,1),c=b[0];if(!d){e.push(function(){j.js.apply(null,a)});return j}c?(q(b,function(a){r(a)||u(p(a))}),v(p(a[0]),r(c)?c:function(){j.js.apply(null,b)})):v(p(a[0]));return j},j.ready=function(a,b){r(a)&&(b=a,a="ALL");var c=g[a];if(c&&c.state==n||a=="ALL"&&s()){b();return j}var d=f[a];d?d.push(b):d=f[a]=[b];return j};function o(a){var b=a.split("/"),c=b[b.length-1],d=c.indexOf("?");return d!=-1?c.substring(0,d):c}function p(a){var b;if(typeof a=="object")for(var c in a)a[c]&&(b={name:c,url:a[c]});else b={name:o(a),url:a};var d=g[b.name];if(d)return d;for(var e in g)if(g[e].url==b.url)return g[e];g[b.name]=b;return b}function q(a,b){if(a){typeof a=="object"&&(a=[].slice.call(a));for(var c=0;c<a.length;c++)b.call(a,a[c],c)}}function r(a){return Object.prototype.toString.call(a)=="[object Function]"}function s(a){a=a||g;for(var b in a)if(a[b].state!=n)return false;return true}function t(a){a.state=k,q(a.onpreload,function(a){a.call()})}function u(a,b){a.state||(a.state=l,a.onpreload=[],w({src:a.url,type:"cache"},function(){t(a)}))}function v(a,b){if(a.state==n&&b)return b();if(a.state==m)return j.ready(a.name,b);if(a.state==l)return a.onpreload.push(function(){v(a,b)});a.state=m,w(a.url,function(){a.state=n,b&&b(),q(f[a.name],function(a){a()}),s()&&q(f.ALL,function(a){a.done||a(),a.done=true})})}function w(c,d){var e=a.createElement("script");e.type="text/"+(c.type||"javascript"),e.src=c.src||c,e.async=false,e.onreadystatechange=e.onload=function(){var a=e.readyState;!d.done&&(!a||/loaded|complete/.test(a))&&(d(),d.done=true)},b.appendChild(e)}setTimeout(function(){d=true,q(e,function(a){a()})},300),!a.readyState&&a.addEventListener&&(a.readyState="loading",a.addEventListener("DOMContentLoaded",handler=function(){a.removeEventListener("DOMContentLoaded",handler,false),a.readyState="complete"},false))})(document)