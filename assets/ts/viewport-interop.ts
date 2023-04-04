const NOTIFY_BREAKPOINT_CHANGED = "BreakpointChangedAsync";
const NOTIFY_INITIALIZED = "InitializedAsync";

let _initialized = false;
let _breakpoint = 0;
let _ref = null;
let _options = {
    breakpoints: {}
}
function getBreakpoint() {
    let width = window.innerWidth;
    if (width >= _options.breakpoints["Xxl"])
        return 5;
    if (width >= _options.breakpoints["Xl"])
        return 4;
    else if (width >= _options.breakpoints["Lg"])
        return 3;
    else if (width >= _options.breakpoints["Md"])
        return 2;
    else if (width >= _options.breakpoints["Sm"])
        return 1;
    else //Xs
        return 0;
}

function notifyBreakpointChanged() {
    _ref.invokeMethodAsync(NOTIFY_BREAKPOINT_CHANGED, _breakpoint);
}

function notifyIfBreakpointChanged() {
    let breakpoint = getBreakpoint();
    if(_breakpoint === breakpoint)
        return;
    _breakpoint = breakpoint;
    notifyBreakpointChanged();
}

function notifyInitialized() {
    _ref.invokeMethodAsync(NOTIFY_INITIALIZED, _breakpoint);
}

export function initialize(ref, options) {
    if(_initialized)
        throw new Error("Breakpoint State Interop: Already initialized");
    if(!ref)
        throw new Error("Breakpoint State Interop: ref is not defined");
    _ref = ref;
    if(!options)
        throw new Error("Breakpoint State Interop: options is not defined");
    _options = options;
    if(!options.breakpoints)
        throw new Error("Breakpoint State Interop: options.breakpoints is not defined");
    _initialized = true;
    window.onresize = notifyIfBreakpointChanged;
    notifyIfBreakpointChanged();
    notifyInitialized();
}
