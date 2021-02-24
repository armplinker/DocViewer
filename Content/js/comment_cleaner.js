//comment_cleaner.js
// author: Nicholas Allen Bickford Marshall, 11/14/2015
// email: nialbima@gmail.com
// https://github.com/nialbima/text_cleaner 
// provide a variety of simple string cleanup operations for tidying up input
// -------------------------------------------------------------------------------

// initialize a Comment object for tests
function Comment(comment_string) {
    if (!comment_string) return null;
    var cleaned_string = clean_comment(comment_string);
    console.log(cleaned_string);
    return cleaned_string;
}

// perform all cleanup operations
// -------------------------------------------------------------------------------
function cleanComment(string) {
    if (!string) return null;
    var clean_string = clearEmpty(string);
    if (clean_string !== '') {
        clean_string = cleanReturns(clean_string);
        clean_string = cleanTabs(clean_string);
        clean_string = trimEnds(clean_string);
    }

    return clean_string;
}

// Return empty string if all whitespace
// -------------------------------------------------------------------------------

function clearEmpty(string) {
    if (!string) return null;
    var str = string;
    if (checkEmpty(str) === true) {
        return '';
    } else {
        return str;
    }

}

// checks to see if there are multiple empties
// this could be combined with the other one below
// -------------------------------------------------------------------------------

function checkEmpty(string) {
    if (!string) return null;
    var str = string;
    if (str === '' || str.match(/^\s*$/)) {
        return true;
    } else {
        return false;
    }
}

// check a rgx
// if true, do something otherwise do something else with the string
// -------------------------------------------------------------------------------

function checkRgx(string, rgx) {
    if (!string || !rgx) return null;
    var str = string;
    if (str.match(rgx)) {
        return true;
    } else {
        return false;
    }
}

// remove extraneous returns from the string (double carriage returns)
// -------------------------------------------------------------------------------

function cleanReturns(string) {
    if (!string) return null;
    var new_str = string;
    var too_many_returns_rgx = /\n{2}\n+/;
    var two_returns = "\n\n";
    if (checkRgx(new_str, too_many_returns_rgx) === true) {
        new_str = new_str.replace(too_many_returns_rgx, two_returns)
        if (checkRgx(new_str, too_many_returns_rgx) === true) {
            cleanReturns(new_str);
        }
    }
    return new_str;
}

// turn tabs into 3 spaces throughout the string
// -------------------------------------------------------------------------------

function cleanTabs(string) {
    if (!string) return null;
    var new_str = string;
    var replace_tabs_rgx = /\t{1}/;
    var replace_with_space = "   ";

    if (checkRgx(new_str, replace_tabs_rgx) === true) {
        new_str = new_str.replace(replace_tabs_rgx, replace_with_space);
        if (checkRgx(new_str, replace_tabs_rgx) === true) {
            cleanTabs(new_str);
        }
    }

    return new_str;
}

// trim any extraneous blanks on the end of the string
// -------------------------------------------------------------------------------

function trimEnds(string) {
    if (!string) return null;
    var new_str = string;
    var trim_ends_rgx = /[\t\r\n\s]+$/;

    if (new_str.match(trimEnds)) {
        new_str = new_str.replace(trim_ends_rgx, "");
        if (new_str.match(trim_ends_rgx)) {
            trimEnds(new_str);
        }
    }

    return new_str;
}
