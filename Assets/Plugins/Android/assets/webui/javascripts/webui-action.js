
var OF,$,NativeInterface,jasmine,U;OF.action=function(actionName,options,callback,immediate){var uri=actionName;if(OF.supports.actionJSON){var json=OF.action.convertToJSON(options);uri=actionName+'?'+encodeURIComponent(json);}
else{for(var key in options){if($.isFunction(options[key])){options[key]=OF.page.saveFunction(options[key]);}}
var query=$.urlEncode(options);if(query.length>0){uri+='?'+query;}}
var actionObj={name:actionName,options:options,uri:uri,callback:callback,immediate:immediate,ignore:OF.action.willIgnoreNext};OF.action.queue.push(actionObj);OF.action.willIgnoreNext=false;var isStalled=OF.action.queue.length>1&&new Date().getTime()-OF.action.lastActionSentAt>5000;if(OF.action.queue.length===1||isStalled){$.defer(OF.action.send);}
if(OF.isBrowser&&actionObj.callback){$.defer(actionObj.callback);}
if(OF.isBrowser&&actionObj.name!=='log'){console.log('ACTION:',actionObj.name,options);}
var spec=typeof jasmine!=='undefined'&&jasmine!==null&&jasmine.getEnv().currentSpec;if(spec){spec.sentActions=spec.sentActions||[];spec.sentActions.push({name:actionName,options:options});}};OF.action.convertToJSON=function(object){return JSON.stringify(object||{},function(key,value){if($.isFunction(value)){return OF.page.saveFunction(value);}
return value;});};OF.action.convertToJSON=function(object){return JSON.stringify(object||{},function(key,value){if($.isFunction(value)){return OF.page.saveFunction(value);}
return value;});};OF.action.now=function(actionName,options,callback){OF.action(actionName,options,callback,true);};OF.action.isSupported=function(actionName){return OF.actions.indexOf(actionName)!==-1;};OF.action.ignoreNext=function(){OF.action.willIgnoreNext=true;};OF.action.delay=75;OF.action.lastActionSentAt=0;OF.sendAction=OF.action;OF.action.queue=[];OF.action.send=function(){var actionObj=OF.action.queue[0];if(OF.isDevice){if(OF.hasNativeInterface){OF.action.send.nativeInterface();}
else if(OF.action.isSupported('batch')){if(actionObj.immediate){OF.action.send.iFrameSingle();}
else{OF.action.send.iFrameBatch();}}
else{OF.action.send.iFrameSingle();}}};OF.action.send.nativeInterface=function(){if(OF.action.queue.length===0){return;}
var actionObj=OF.action.queue.shift();if(actionObj.ignore){return;}
var uri='openfeint://action/'+actionObj.uri;NativeInterface.action(uri);if(actionObj.callback){$.defer(actionObj.callback);}
setTimeout(OF.action.send,OF.action.delay);};OF.action.send.iFrameSingle=function(){if(OF.action.queue.length===0){return;}
var actionObj=OF.action.queue.shift();if(actionObj.ignore){return;}
var actionPath=actionObj.uri;var uri='openfeint://action/'+actionPath;$.defer(function(){$('#action_frame').attr('src',uri);if(actionObj.callback){$.defer(actionObj.callback);}});if(OF.platform=='ios'){if(uri.match(/contentLoaded/)){setTimeout(function(){$('#action_frame').attr('src',uri);},250);}}
setTimeout(OF.action.send,OF.action.delay);OF.action.lastActionSentAt=new Date().getTime();};OF.action.send.iFrameBatch=function(){if(OF.action.queue.length===1){OF.action.send.iFrameSingle();return;}
else if(OF.action.queue.length===0){return;}
var actions=[];$.each(OF.action.queue,function(i,actionObj){if(actionObj.ignore){return;}
var name=actionObj.name;var options=actionObj.options||{};actions.push({name:name,options:options});});var json=OF.action.convertToJSON({actions:actions});var uri='openfeint://action/batch?'+encodeURIComponent(json);$.defer(function(){$('#action_frame').attr('src',uri);});$.each(OF.action.queue,function(i,actionObj){if(actionObj.callback){$.defer(actionObj.callback);}});OF.action.queue=[];};OF.action.send.iFrameBatch=U.debounce(OF.action.send.iFrameBatch,250);OF.actionIsSupported=$.deprecate(OF.action.isSupported,'OF.actionIsSupported()','OF.action.isSupported()');