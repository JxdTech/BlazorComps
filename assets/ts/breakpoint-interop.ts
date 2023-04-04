const NOTIFY_BREAKPOINT_CHANGED = "OnBreakpointChangedAsync";
const NOTIFY_INITIALIZED = "OnInitializedAsync";

let _ref = null;
let _initialized = false;
let _breakpoint = 0;
let _breakpoints = {};
function getBreakpoint() {
    let width = window.innerWidth;
    if (width >= _breakpoints["Xxl"])
        return 5;
    if (width >= _breakpoints["Xl"])
        return 4;
    else if (width >= _breakpoints["Lg"])
        return 3;
    else if (width >= _breakpoints["Md"])
        return 2;
    else if (width >= _breakpoints["Sm"])
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

export function initialize(ref, breakpoints) {
    if(_initialized)
        throw new Error("breakpoint-interop: Already initialized");
    if(!ref)
        throw new Error("breakpoint-interop: ref is not defined");
    _ref = ref;
    if(!breakpoints)
        throw new Error("breakpoint-interop: breakpoints are not defined");
    _breakpoints = breakpoints;
    _initialized = true;
    window.onresize = notifyIfBreakpointChanged;
    notifyIfBreakpointChanged();
    notifyInitialized();
}
