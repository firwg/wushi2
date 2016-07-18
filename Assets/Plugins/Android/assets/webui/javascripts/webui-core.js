var OF, NativeInterface, $, jQuery, tmpl, runSpecs, U, jasmine; // Implied Globals


// Prevent an expection when accessing the runSpecs var when not running specs
if (typeof(runSpecs) === 'undefined') {
  var runSpecs = false;
}

// Root OF object that all context is attached to.
var OF = {
  // True if web view is on an client device
  isDevice:    false,

  // True if in a web browser on a computer
  isBrowser:  false,

  // simple boolean to query what platform we are on
  deviceType: {
    iOS:      navigator.userAgent.match(/iPhone|iPad/),
    Android:  navigator.userAgent.match(/Android/)
  },

  // True if there is an object defined by "NativeInterface" that allows
  // javascript to directly interface with a native code object on the client
  hasNativeInterface: false,

  // The current page object
  page: null,

  // All pages in the flow are in this array.  The n-1 index is the currently
  // shown page, and zero index is the first page in the flow.
  pages: [],

  // Any data set here is preserved across all pages in the flow
  global: {},

  // "landscape" or "portrait".  This is set by the client via:
  //    OF.setOrientation(value)
  orientation: null
};

// Removes the current flow and sets a brand new root page with the one at "path"
OF.pages.replace = function(path) {
  OF.pages.splice(0, OF.pages.length);
  OF.push(path);
};

// Set device/browser flags
OF.isDevice = navigator.userAgent.match(/iPhone|iPad|Android/);
OF.isBrowser = !OF.isDevice;

// Legacy alias
OF.navigationStack = OF.pages;

OF.truncate = function(string, maxLength) {
  if (string.length > maxLength) {
    string = string.substring(0, maxLength) + '...';
    return string;
  } else {
    return string;
  }
}


// Log out a message.  Use instead of console.log because it shows up in the
// client's debug log (i.e. Xcode)
OF.log = function(data) {
  if (OF.isBrowser) {
    console.log('WEBLOG:', data);
  }

  // iOS 3.x seems get clogged with too many actions being passed around.
  // So give the poor thing a break, will ya?  Geez.
  if (OF.device.ios3) {
    return;
  }

  // Send a log action to the client to show in the XCode console
  var message;
  if (OF.isDevice) {
    if (typeof data === 'object') {
      // break down the object.
      message = $.urlEncode(data);
    } else {
      // force into to a string
      message = ''+ data;
    }

    OF.sendAction('log', {
      message: message
    });
  }
};

// Menu handling functions
OF.menu = function(buttonName) {
  if      (buttonName == 'home')      OF.menu.home();
  else if (buttonName == 'settings')  OF.menu.settings();
  else if (buttonName == 'exit')      OF.menu.exit();
  else {
    OF.log('Unknown menu button hit: '+ buttonName);
  }
};

OF.menu.home = function() {
	$('.footer_menu').find('.footer_tab:first').addClass("active");
  if (OF.navigationStack[0].url.match(/dashboard\/user/)) {
    OF.goBack({root:true});
  } else {
    
    // This is a bit of a hack for now...
    OF.navigateToUrl("dashboard/user", {
      complete: function() {
        OF.pages.unshift(OF.pages.pop());
        OF.action('back', {root:true}, OF.contentLoaded);
      }
    });
  }
};

OF.menu.settings = function() {
  OF.sendAction('openSettings');
};

OF.menu.exit = function() {
  OF.sendAction('dismiss');
};

OF.setActiveFootermenu = function(){
	if(OF.user.id){
    $('.footer_menu .footer_tab').removeClass("active");
    if(typeof(OF.topPage()) != 'undefined' && OF.topPage().url == 'dashboard/recommend_games.json'){
      $('.footer_menu').find('.footer_tab:last').addClass("active");
    }else{
      $('.footer_menu').find('.footer_tab:first').addClass("active");
    }
	  $('.footer_menu .footer_tab').touch(function(){
	    $('.footer_menu .footer_tab').removeClass("active");
			var i = $('.footer_menu .footer_tab').index($(this));
	    if( i == 0 || i == 2 ) {
		    if(OF.sdkSupportThe9Download){
					 OF.menu.home();
		    }else{
					OF.sendAction("dashboard");
		    }
	    } else {
		    $('.footer_menu').find('.footer_tab:last').addClass("active");
		    if(typeof(OF.topPage()) != 'undefined' && OF.topPage().url != 'dashboard/recommend_games.json'){
	        OF.push("dashboard/recommend_games");
	      }
	    }
	  });
  }
}

OF.setFootermenu = function(){
	if(typeof(OF.topPage()) != 'undefined' && (OF.topPage().url == 'dashboard/game.json')){
		if($('#cp_buy')[0].style.display != 'block' && OF.orientation == 'landscape'){
			$('.subtitle_for_game').css({'width':'50%','overflow' : 'hidden', 'text-overflow': 'ellipsis','white-space': 'nowrap'})
		}
		if($('#cp_buy')[0].style.display != 'block' && OF.orientation != 'landscape'){
			$('.subtitle_for_game').css({'width':'40%','overflow' : 'hidden', 'text-overflow': 'ellipsis','white-space': 'nowrap'})
		}
	}
	
	if(OF.user.id){
		// var supportfixedPosition = true
	  var supportfixedPosition = (OF.platform === 'android' && OF.device.os.match(/(v2\.[2-9])|(v[3-9]\.)|(v[1-9]([0-9]+)\.)/));
    if(OF.orientation == 'landscape' && supportfixedPosition && (typeof(OF.topPage()) != 'undefined' && (OF.topPage().url == 'settings/email.json' || OF.topPage().url == 'settings/name.json' || OF.topPage().url == 'settings/password.json' || OF.topPage().url == 'dashboard/user_search.json' || OF.topPage().url == 'dashboard/guests.json' || OF.topPage().url == 'dashboard/reviews/new.json' || OF.topPage().url == 'dashboard/new_status.json'))){
      $('.footer_menu').hide();
    }else{
      // $('.footer_menu').show();
      if(supportfixedPosition){
				$('#footer_menu_for_2_2').css('z-index','9999');
        $('#footer_menu_for_2_2').css({'position':'fixed','bottom':'0px','left':'0px'});
				$('#footer_menu_for_2_2').unhide();
				$('#footer_menu_for_2_1').hide();
      }else{
				$('#footer_menu_for_2_2').hide();
				$('#footer_menu_for_2_1').css({'position':'absolute','top':'62px'});
				$('#footer_menu_for_2_1').unhide();
			}
    }
  }else{
    $('.footer_menu').hide();
  }
}
// Set orienation to "landscape" or "portrait".  This is called by the client
// whenever an orientation change ocurrs.
OF.setOrientation = function(newOrientation) {
  if (newOrientation) {
    OF.orientation = newOrientation;		
		OF.setFootermenu();
	
    $('body')
    .removeClass('orientation_portrait')
    .removeClass('orientation_landscape')
    .addClass('orientation_'+ OF.orientation);

    if (OF.topPage() && OF.topPage().orientationChanged) {
      OF.topPage().orientationChanged(OF.orientation);
    }
  }
};

// These functions handle page load tasks
OF.init = {

  // true if we are not loading any content
  isLoaded: false,
  flowIsLoaded: false,

  // Fetch the first content
  firstPage: function() {
    var queryString = location.href.split('?')[1];
    var hasUrl = false;
    if (queryString) {
      var options = $.urlDecode(queryString);

      if (options.url) {
        hasUrl = true;
        $.ajax({
          url:'/webui/browser_config.json',
          dataType: 'json',
          complete: function(xhr) {
            var data = $.parseJSON(xhr.responseText);
            OF.init.clientBoot(data);
            OF.push(options.url);
          }
        });
      }
    }

    // If we are loading in the browser and we have no an alert telling you
    // that a url is needed.
    if (OF.isBrowser && !hasUrl) {
      OF.alert(_('ERROR'), _("No Content to Load! This page must be called with a url like /webui/?url=some/content_path"));
    }

    // Allow selection in the browser
    if (OF.isBrowser) {
      $('html, body').css('-webkit-user-select', 'auto');
    }

    // Create generic touch handlers for elements with data-* declared touch functions
    OF.touch.bindHandlers();

    // Don't allow touches while scrolling is happening
    var win = $(window);
    win.scroll(function() {
      OF.touch.isScrolling = true;
      clearTimeout(win.data('stopScrollingCallback'));

      if (OF.page.eventHandle) {
        OF.page.eventHandle().trigger("scroll");
      }

      var timer = setTimeout(function() {
        OF.touch.isScrolling = false;
      }, 250);

      win.data('stopScrollingCallback', timer);
    });
  },

  clientBoot: function(options) {
    OF.hasNativeInterface = options.hasNativeInterface;

    OF.user     = options.user;
    if (!OF.user.name) {
      OF.user.name = null;
    }
    if (!OF.user.id || OF.user.id.toString() === '0') {
      OF.user.id = null;
    }

    OF.game       = options.game;
    OF.serverUrl  = options.serverUrl;

    OF.actions  = options.actions;
    OF.platform = options.platform;
    OF.device   = options.device;

    // Hack for slow iOS devices to behave themselves
    OF.device.ios3 = !!OF.device.os.match(/iPhone.*3\.\d\.\d/);
    if (OF.device.ios3) {
      OF.action.delay = 250;
    }

    OF.clientVersion = options.clientVersion;
		OF.sdkSupportThe9Download = (OF.clientVersion != '1.7.1' && OF.clientVersion != '1.8');

    OF.dpi      = options.dpi;
    OF.setOrientation(options.orientation);


    OF.locale   = options.locale ? options.locale.replace(/_/g, '-') : 'en-US';
    //OF.i18n.loadLocale(OF.locale.split('-')[0], OF.locale.split('-')[1]);

    OF.disableGA = options.disableGA;
    OF.settings.enabled = OF.action.isSupported('readSetting') && OF.action.isSupported('writeSetting');


    OF.supports = options.supports || {};

    OF.supports.fixedPosition = ((OF.platform === 'android' && OF.device.os.match(/(v2\.[2-9])|(v[3-9]\.)|(v[1-9]([0-9]+)\.)/)) || OF.device.hardware === 'browser'); // TODO: this regex is very brittle.

    // Supplied by browser_config.json, for in browser testing of non embed poducts
    OF.manifestUrl = options.manifestUrl;

    OF.log("Client Booted - userID:"+ OF.user.id +" gameID:"+ OF.game.id +" platform:"+ OF.platform +" dpi:"+ OF.dpi);

    // Add classes to body element to allow various hardware and platform CSS customizations
    var body = $('body');
    body.addClass(OF.dpi).addClass(OF.platform);
    if (OF.supports.fixedPosition) {
      body.addClass('fixed_position');
    }

    return true;
  },

  // Get the page ready for the client
  start: function() {
    OF.init.isLoaded = false;
    OF.init.translate();
    OF.init.scripts();
    OF.init.browser();
    OF.init.params();

    // Load up the flow, if needed
    if (!OF.init.flowIsLoaded && OF.page.loadflow) {
      OF.page.loadflow();
      OF.init.flowIsLoaded = true;
    }

    if (!OF.page.init) {
      OF.page.init = $.noop;
    }
    if (OF.page.init.complete) {

      // Aleady ran init() so run resume() instead
      if (OF.page.resume) {
        $.defer(function() {
          try {
            OF.page.resume();
          } catch(e) {
            OF.alert(_('ERROR'), _('A script on this screen caused an error.\n appear: %1', e.toString()));
          }
        });
      }

    } else {

      // Haven't yet run init()
      $.defer(function() {
        try {
          OF.page.init();
        } catch(e) {
          OF.alert(_('ERROR'), _('A script on this screen caused an error.\n appear: %1', e.toString()));
        }
        OF.page.init.complete = true;
      });
    }

    $.defer(function() {
      if (OF.page.appear) {
        try {
          OF.page.appear();
        } catch(e) {
          OF.alert(_('ERROR'), _('A script on this screen caused an error.\n init: %1', e.toString()));
        }
      }
    });
    
    $.defer(OF.init.pageViewTracking);
    OF.init.isLoaded = true;

    var buttonTitle = OF.page.barButton || OF.page.globalBarButton;
    var options = {};
    if (buttonTitle) { options.barButton = buttonTitle; }
    
    $.defer(function() {
      if (!OF.device.ios3 || (OF.device.ios3 && OF.api.activeRequestIDs.length === 0)) {
        document.title = OF.page.title;
        OF.contentLoaded(options);
      }
    });

  },

  // translate elements marked as translatable
  translate: function() {
    if (OF.i18n.gt) {
      $.defer(function() {
        if (OF.page) {
          OF.page.title = _(OF.page.title);
          document.title = OF.page.title;
        }

        $.each($('.t'), function() {
          var elem = $(this);
          elem.html(_(elem.html()));
          elem.removeClass('t');
        });
      });
    } else {
      OF.log('Gettext not loaded, stalling...');
      setTimeout(OF.init.translate, 50);
    }
  },

  // Compile string style javascript from JSON into functions
  scripts: function() {
    if (OF.page.init && typeof(OF.page.init) !== 'function') {
      OF.page.init = $.functionize(OF.page.init, OF.page.url, 'init');
      OF.page.init.complete = false;
    }

    if (OF.page.appear && typeof(OF.page.appear) !== 'function') {
      OF.page.appear = $.functionize(OF.page.appear, OF.page.url, 'appear');
    }

    if (OF.page.resume && typeof(OF.page.resume) !== 'function') {
      OF.page.resume = $.functionize(OF.page.resume, OF.page.url, 'resume');
    }

    if (!OF.init.flowIsLoaded && OF.page.loadflow && typeof(OF.page.loadflow) !== 'function') {
      OF.page.loadflow = $.functionize(OF.page.loadflow, OF.page.url, 'loadflow');
    }
  },

  // Show the browser toolbar, usable only when NOT on a device
  browser: function() {
    if (OF.isBrowser && $('#browser_toolbar').length === 0) {
      $.loadCss('browser_toolbar', false);
      $.get('browser_toolbar.html', function(data) {
        $(document.body).append(data);
      });
    }
  },

  // Creates nav bar button
  barButton: function() {
    var options = {};
    var buttonName = OF.page.barButton || OF.page.globalBarButton;

    if (OF.page.barButton)       {
      options.barButton      = buttonName;
    }
    if (OF.page.barButtonImage)  {
      options.barButtonImage = OF.page.barButtonImage;
    }
    OF.action('addBarButton', options);
  },

  // Google analytics page track for the just loaded page
  pageViewTracking: function() {
    if (OF.topPage()) {
      OF.GA.page("/webui/" + OF.topPage().url);
    }
  },

  // Populate page.query with query string object
  params: function() {
    var page = OF.topPage();
    if (!page.params) {
      page.params = {};
    }
    if (page.url.match(/\?/)) {
      $.extend(page.params, $.urlDecode(page.url.split('?')[1]));
    }
  }
};

// I18N support
OF.i18n = {

  // OF.i18n.gt holds the Gettext object
  gt: new Gettext({
    domain: "zh",
    locale_data: GettextJsonData
    }
  ),

  translate: function(str) {
    var args = Array.prototype.slice.call(arguments);
    args.shift();

    return Gettext.strargs(OF.i18n.gt.gettext(str), args);
  },

  translatePlural: function(singular, plural, count) {
    var args = Array.prototype.slice.call(arguments);
    args.shift(); // remove singular msgid
    args.shift(); // remove plural msgid

    return Gettext.strargs(OF.i18n.gt.ngettext(singular, plural, count), args);
  },

  // Load local data given a language and country.
  // successCallback is called once loaded.
  loadLocale: function(lang, country, successCallback) {

    // Locales to try to load.  Going from the most specific to the
    // most general
    locales = [];
    if (country) locales.push(lang.toLowerCase() +'-'+ country.toUpperCase());  // lang-COUNTRY
    locales.push(lang.toLowerCase());                                           // lang only
    locales.push('en-US');                                                      // Default locale
    locales = ['zh'];
    // Successfully loaded locale, load into Gettext
    var success = function(localeData, localeName) {
      OF.i18n.gt = new Gettext({
        domain: localeName,
        locale_data: localeData
      });
      OF.i18n.gt.textdomain(localeName);

      if (successCallback) {
        successCallback();
      }
    };

    // fall back to a more general locale on a failure to load
    var loadNextLocale = function() {
      var locale = locales.shift();
      if (locale) {
        OF.i18n._loadLocale(locale, success, loadNextLocale);
      } else {
        OF.alert('ERROR', 'Failed to load any locales!');
      }
    };

    // try to load thefirst and most specific locale data
    loadNextLocale();
  },

  // Load a single locale and report success of failure
  _loadLocale: function(name, successCallback, failureCallback) {
    $.ajax({
      url:      'javascripts/locales/'+ name + '.json?'+ new Date().getTime(),
      dataType: 'json',
      success:  function(data) {
        successCallback(data, name);
      },
      error:    failureCallback
    });
  }
};

// Force-sets the title of the webui view
OF.forceSetTitle = function(title){
  if ($('#header .title').length > 0) {
    $('#header .title').html(title);
  }
};

// Shorthand for getting the top page object
OF.topPage = function() {
  return OF.pages[OF.pages.length-1];
};

// Legacy alias
OF.topNavigationItem = $.deprecate(OF.topPage, 'OF.topNavigationItem()', 'OF.topPage()');

// Loads the top page object on the navigation stack
OF.loadTopPage = function(completionCallback) {

  // Get the top page
  var page = OF.topPage();
  
  // Set screen state
  OF.page = page;

  // Clear things out to start clean
  $('#page').html('&nbsp;');

  // Defer initialization to allow webview to redraw itself
  setTimeout(function() {
    // Continue initialization in another method that allows it to loop
    // until the client is actually ready with the content loaded in its DOM
    OF.loadTopPage.htmlReady(page, completionCallback);
  }, 50);
};

// Writes the pages html into the DOM.  Then is double checks that it worked,
// and if not then retry in a few milliseconds. Mobile browsers have been
// known to lock the DOM during page navigations, and this ensures the page
// update really works.
OF.loadTopPage.htmlReady = function(page, completionCallback) {
  // If no nodes, then use original html instead
  if (!page.nodes) {
    page.nodes = page.html;
  }
  
  // Wrap HTML if it's coming from a string
  if (typeof(page.nodes) === 'string') {
    page.nodes = ['<div id="event_context">', page.html, '</div>'].join('');
  }

  // Load HTML content

  $('#page')
    .html(page.nodes)
    .append("<div class='eventHandle' />")
    .attr('data-page_id', page.id);
  
  setTimeout(function() {
    // Ensure the client has loaded the HTML
    if ($.trim($('#page').html() || '').length === 0) {
      OF.log("Retrying... HTML not yet ready.");
      OF.loadTopPage.htmlReady(page); 
    // HTML is loaded, continue initialization
    } else {
      // Save the event context
      OF.topPage().eventContext = $('#event_context')[0];

      // Perform initial page load javascript
      OF.init.start();
      if (completionCallback) { $.defer(completionCallback, page); }
      // Scroll to the previous position
      $.defer(function() {
        window.scroll(0, page.scrollPosition);
      });

      // Run the specs
      if (runSpecs) {
        OF.specs.run();
      }
    }
  }, 100);
};

// Open a URL as a new page pushed onto the navigation stack
OF.push = function(url, options) {
  OF.init.isLoaded = false;
  if (!options) {
    options = {};
  }

  var onComplete = options.complete;
  options.complete = null;

  // Ensure url is requesting .json
  url = $.jsonifyUrl(url);
  options.path = url;
  OF.log("Loading content: "+ url);

  // Save important page state before adding another page
  var params = options.params || {};
  options.params = null;

  if (OF.pages.length > 0) {
    OF.topPage().scrollPosition = window.scrollY;
  }

  OF.specs.load(url);

  OF.push.ready = function(pageJSON) {
    var loadPage = function(data) {
      if (!OF.init.isLoaded) {
        if (OF.page) {
          OF.page.nodes = $('#page').contents().detach();
        }

        var pageData = $.clone(data);
        $.extend(pageData, OF.pageFunctions);

        pageData.url = url;
        pageData.id = ['page', url.replace(/\W/g, '-'), (new Date().getTime())].join('_');
        pageData.scrollPosition = 0;
        pageData.modal = options.modal;
        OF.pages.push(pageData);
        OF.loadTopPage(onComplete);
        OF.topPage().params = $.extend(OF.topPage().params, params);

        // Initialize analytics
        OF.GA.init();
      }
    };

    $.defer(function() {
      if (pageJSON) {
        // Client provided the page content, just load it
        loadPage(pageJSON);
      } else {
        // Request new page content via ajax
        $.ajax({
          url: url,
          dataType: 'json',
          success: loadPage,
          error: function(xhr) {
            OF.init.isLoaded = true;
            OF.alert(_("Error"), _("Screen loading failed:\n%1 %2", xhr.status, xhr.statusText));
            OF.loader.hide();
          }
        });
      }
    });
  };

  // Legacy alias
  OF.navigateToUrlCallback = OF.push.ready;

  OF.action.now('startLoading', options);
  OF.loader.show();
  if (OF.isBrowser) {
    OF.push.ready();
  }
};

// Legacy alias
OF.navigateToUrl = $.deprecate(OF.push, 'OF.navigateToUrl()', 'OF.push()');


// reload the current page
OF.refresh = function() {
  if (OF.page) {
    OF.page.nodes = null;
    OF.page.init.complete = false;
    $.defer(OF.loadTopPage);
  }
};

// Push a native controller onto the stack
OF.pushController = function(controllerName, options) {
  controllerName = controllerName +'?'+ $.urlEncode(options);
  if (OF.isDevice) {
    location.href = 'openfeint://controller/'+ controllerName;
  }
  OF.log('CONTROLLER:'+ controllerName);
};

// Client action that tells it a page is ready for viewing. When this fires
// all html, scripts, and images are loaded and ready
OF.contentLoaded = function(options) {
  if (!options) {
    options = {};
  }

  options.title = document.title || OF.page.title;
  if (OF.page.titleImage)      { options.titleImage      = OF.page.titleImage; }
  if (OF.page.barButton)       { options.barButton       = OF.page.barButton; }
  if (OF.page.barButtonImage)  { options.barButtonImage  = OF.page.barButtonImage; }
  OF.loader.hide();
  OF.action('contentLoaded', options);
};

// Set a bar button on iOS and a handler for it.  Title can be a string or
// a WebUI relative image path
OF.barButton = function(title, onTouch) {
  var options = {};
  if (title.match(/png$/)) {
    options.image = title.replace('xdpi.png', OF.dpi +'.png');
  } else {
    options.title = title;
  }

  $.defer(function() {
    OF.page.barButtonTouch = onTouch;
    OF.action('addBarButton', options);
  });
};

// Pop the top navigation item, and load the one behind it
OF.goBack = function(options) {
  OF.touch.cancel();
 
  if (!options) {
    options = {};
  }
  if (OF.init.isLoaded && OF.pages.length > 1) {
    if (options.root) {
      OF.pages.splice(1, OF.pages.length - 1);
    } else {
      OF.pages.pop();
    }
    var onComplete = options.complete;
    delete options.complete;

    OF.action.now('back', options, function() {
      OF.loadTopPage(onComplete);
    });
  } else {
    if (!OF.device.ios3 || (OF.device.ios3 && OF.api.activeRequestIDs.length === 0)) {
      OF.contentLoaded(options);
    }
  }
  OF.init.isLoaded = true;
  OF.loader.hide();
};

OF.alert = function(title, message, options) {
  options = options || {};
  options.title = title;
  options.message = message;
  OF.action('alert', options);

  // browser alert if in the browser
  if (OF.isBrowser) {
    alert(options.title +"\n\n"+ options.message);
  }
};

OF.confirm = function(title, message, positive, negative, positiveCallback, negativeCallback) {
  OF.action('confirm', {
    title: title,
    message: message,
    positive: positive,
    negative: negative,
    callback: function(result) {
      if (result) {
        positiveCallback();
      } else {
        if (negativeCallback) {
          negativeCallback();
        }
      }
    }
  });

  if (OF.isBrowser) {
    if (confirm(title +"\n\n"+ message)) {
      positiveCallback();
    } else {
      if (negativeCallback) {
        negativeCallback();
      }
    }
  }
};

OF.loader = {
  count: 0,
  show: function() {
    if (OF.device.ios3) {
      return;
    }
    if (OF.loader.count === 0) {
    // OF.action('showLoader');
    }
    $('#header .loading').show();
    OF.loader.count += 1;
  },
  hide: function() {
    if (OF.device.ios3) {
      return;
    }
    OF.loader.count -= 1;
    if (OF.loader.count < 0) {
      OF.loader.count = 0;
    }
    if (OF.loader.count === 0) {
      $('#header .loading').hide();
    // OF.action('hideLoader');
    }
  }
};

// Store (or clear) the logged in user.
OF.userDidLogin = function(user) {
  if (user && user.id && user.id.length && user.id.toString() !== '0') {
    OF.user = user;
  } else {
    OF.user = {
      name: null,
      id: null
    };
  }

  if (OF.page && OF.page.userDidLogin) {
    OF.page.userDidLogin(user);
  }
};

OF.pageFunctions = {
  // hold an object filled with saved functions so the client can
  // use them as callbacks.
  savedFunctions: {},

  eventHandle: function() {
    return $("#page > .eventHandle");
  },
  saveFunction: function(fn) {
    if ($.isFunction(fn)) {
      var string = U.uniqueId('saved_func_');
      this.savedFunctions[string] = fn;
      return ['OF.pages[', OF.pages.indexOf(this), '].savedFunctions.',  string].join('');
    }
  }
};

OF.settings = {
  // Flag provides an easy way to tell if the settings API is supported on this client
  enabled: false,

  expectJsonAsString: null,
  
  clear: function(key) {
    OF.settings.write(key, null);
  },
  
  write: function(key, value) {
    OF.action('writeSetting', { key: key, value: JSON.stringify(value) });
    
    if (OF.isBrowser) {
      document.cookie = key + "=" + encodeURIComponent(JSON.stringify(value));
    }
  },

  read: function(key, callback) {
    // Android clients 1.7.5 and earler had a problem with JSON escaping.
    // Values returned from these clients need to be parsed as JSON.
    if (OF.settings.expectJsonAsString === null) {
      var clientVersion = OF.clientVersion ? OF.clientVersion.split('.') : [0,0,0]
      
      OF.settings.expectJsonAsString = (
        OF.platform === 'android' &&
        (parseInt(clientVersion[0], 10) || 0) <= 1 &&
        (parseInt(clientVersion[1], 10) || 0) <= 7 &&
        (parseInt(clientVersion[2], 10) || 0) <= 5
      );
    }
    
    if (OF.settings.expectJsonAsString) {
      var origCallback = callback;
      callback = function(jsonStringVal) {
        origCallback(jsonStringVal === null ? null : JSON.parse(jsonStringVal));
      };
    }
    
    OF.action('readSetting', { key: key, callback: callback });
    
    // Callback with cookie value in browser

    if (OF.isBrowser || !OF.settings.enabled) {
      var foundSetting = false;
      U(document.cookie.split('; ')).each(function(cookie) {
        var cName = cookie.split('=')[0];
        if (cName === key) {
          foundSetting = true;
          $.defer(function() {
            var value = JSON.parse(decodeURIComponent(cookie.split('=')[1]));
            callback(value);
          });
        }
      });
      
      if (!foundSetting) {
        callback(null);
      }
    }
  }
};

// Find specs for this flow and run them
OF.specs = {
  load: function(pagePath) {
    if (runSpecs) {
      // prevent api requests during load
      OF.api.allow = false;

      // Find the current flow
      var flow = pagePath.split('/')[0];

      // load the spec index for this flow
      $.loadScript('../spec/'+ flow +'/index');

      // override waitsFor() increment value
      jasmine.WaitsForBlock.TIMEOUT_INCREMENT = OF.isDevice ? 500 : 200;
      if (OF.device.ios3) {
        jasmine.WaitsForBlock.TIMEOUT_INCREMENT = 1000;
        jasmine.DEFAULT_TIMEOUT_INTERVAL        = 10000;
      }
    }
  },

  run: function() {
    if (runSpecs && OF.pages.length === 1) {
      // Ensure we dont do this again
      runSpecs = false;
    }
  }
};

if (typeof NativeInterface === "object" && NativeInterface.frameworkLoaded) {
  NativeInterface.frameworkLoaded();
}


// Convenience function for translations
var  _ = OF.i18n.translate;
var n_ = OF.i18n.translatePlural;

// --- GO ---
$(document).ready(OF.init.firstPage);
