// Comment.js
// specs found in spec/CommentSpec.js
//
// author:Nicholas Allen Bickford Marshall, 11/14/2015
// email:nialbima@gmail.com
// https://github.com/nialbima/text_cleaner
// provide a variety of simple string cleanup operations for tidying up input
// -------------------------------------------------------------------------------

// this prototype is used by Jasmine for testing.
// takes a string, and an optional options hash.
// -------------------------------------------------------------------------------
function Comment(commentString, options) {
    var cleanedString = commentString;

    if (options) {

        //if it doesn't exist, set to 3. redundant with the code below.
        if (!options['tabsValue']) {
            options['tabsValue'] = 3;
        }

        // if not provided, defaults to 0, and does not fire below.
        if (!options['truncateValue']) {
            options['truncateValue'] = 0;
        }
        cleanedString = cleanComment(commentString, options);
    }

    this.text = cleanedString;
    return cleanedString;
}

// invokes all the cleaning functions in order
// accepts an options hash
// -------------------------------------------------------------------------------

function cleanComment(stringVal, options) {
    if (!stringVal) return null;

    var tabString = "";
    var cleanString = clearEmpty(stringVal);
    //when called as a literal as opposed ot part of the constructor, default value
    if (options) {
        tabString = setTabString(options['tabsValue']);
    } else {
        tabString = "   ";
    }

    // skip everything if it's empty
    if (cleanString !== '') {
        cleanString = cleanReturns(cleanString);
        cleanString = cleanTabs(cleanString, tabString);
        cleanString = trimEnds(cleanString);
    }

    // you can set a max-length flag with truncateValue
    if (options) {
        if (options['truncateValue'] > 0) {
            cleanString = cleanString.substring(0, options['truncateValue']);
        }
    }

    return cleanString;
}

// if tabsValue is provided, sets tabString to be used with cleanTabs below.
// defaults to 3 spaces.
// -------------------------------------------------------------------------------

function setTabString(tabsValue) {
    var tabStringLength = tabsValue || 3;
    var tabString = "";

    // max 10 spaces for tabs
    if (tabStringLength > 10) {
        tabStringLength = 10;
    }

    for (i = 0; i < tabStringLength; i++) {
        tabString += " ";
    }

    return tabString;
}

// if the string is empty, replaces the whole thing with ''.
// -------------------------------------------------------------------------------

function clearEmpty(stringVal) {
    if (checkEmpty(stringVal) === true) {
        return '';
    } else {
        return stringVal;
    }
}

// checks to see if the string is empty.
// -------------------------------------------------------------------------------

function checkEmpty(stringVal) {
    if (stringVal.match(/^\s*$/)) {
        return true;
    } else {
        return false;
    }
}

// tests a string against a supplied regex, returns a boolean
// controls whether to invoke the cleaning functions
// -------------------------------------------------------------------------------

function checkRgx(stringVal, rgx) {
    if (stringVal.match(rgx)) {
        return true;
    } else {
        return false;
    }
}

//replaces \n when there's more than 2 of them in a row
// -------------------------------------------------------------------------------

function cleanReturns(stringVal) {
    const str = stringVal;
    const tooManyReturnsRegex = /\n{2}\n+/g;
    const twoReturns = "\n\n";

    if (checkRgx(stringVal, tooManyReturnsRegex) === true) {
        var newStr = stringVal.replace(tooManyReturnsRegex, twoReturns);
        if (checkRgx(newStr, tooManyReturnsRegex) === true) {
            // recursive to capture all errors in the string
            cleanReturns(newStr);
        }
        return newStr;
    } else {
        return str;
    }
}

// replaces \t with tabString (# of spaces, provided by setTabString)
// -------------------------------------------------------------------------------

function cleanTabs(stringVal, tabString) {
    const str = stringVal;

    const replaceTabRegex = /\t{1}/g;
    const replaceWithSpace = tabString;

    if (checkRgx(stringVal, replaceTabRegex) === true) {
        var newStr = stringVal.replace(replaceTabRegex, replaceWithSpace);
        if (checkRgx(newStr, replaceTabRegex) === true) {
            // recursive to capture all errors in the string
            cleanTabs(newStr);
        }
        return newStr;
    } else {
        return str;
    }

}

// tests for any excess space at the end of the string, replaces with ""
// -------------------------------------------------------------------------------

function trimEnds(stringVal) {
    const str = stringVal;
    const trimEndsRegex = /[\t\r\n\s]+$/g;

    // if the string has any number of spaces at the end, strip them.
    if (stringVal.match(trimEndsRegex)) {
        var newStr = stringVal.replace(trimEndsRegex, "");
        if (newStr.match(trimEndsRegex)) {
            // recursive
            trimEnds(newStr);
        }
        return newStr;
    } else {
        return str;
    }
}
