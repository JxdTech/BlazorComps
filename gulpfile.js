const gulp = require("gulp");
const sass = require("gulp-sass")(require("sass"));
const sourcemaps = require('gulp-sourcemaps');
const autoprefixer = require('gulp-autoprefixer');
const jsmin = require('gulp-jsmin');
const cssmin = require('gulp-cssmin');
const rename = require('gulp-rename');
const ts = require('gulp-typescript');

const COMPONENTS_STYLES_REGEX = './assets/scss/**/*.scss';
const COMPONENTS_STYLES_OUTPUT_DIR = './src/wwwroot/css';

const COMPONENTS_SCRIPTS_REGEX = './assets/ts/**/*.ts';
const COMPONENTS_SCRIPTS_OUTPUT_DIR = './src/wwwroot/js';

function buildStyles() {
    return gulp.src(COMPONENTS_STYLES_REGEX)
        .pipe(sass().on("error", sass.logError))
        .pipe(sourcemaps.init())
		.pipe(autoprefixer())
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(COMPONENTS_STYLES_OUTPUT_DIR));
}

function buildMinifiedStyles() {
    return gulp.src(COMPONENTS_STYLES_REGEX)
        .pipe(sass().on("error", sass.logError))
        .pipe(sourcemaps.init())
		.pipe(autoprefixer())
        .pipe(cssmin())
        .pipe(rename({suffix: '.min'}))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(COMPONENTS_STYLES_OUTPUT_DIR));
}

var styles = gulp.parallel(buildStyles, buildMinifiedStyles);

function buildScripts() {
    return gulp.src(COMPONENTS_SCRIPTS_REGEX)
        .pipe(sourcemaps.init())
        .pipe(ts({
            target: 'ES6',
            module: 'es6'
        }))
        .on('error', console.error)
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(COMPONENTS_SCRIPTS_OUTPUT_DIR));
}

function buildMinifiedScripts() {
    return gulp.src(COMPONENTS_SCRIPTS_REGEX)
        .pipe(sourcemaps.init())
        .pipe(ts({
            target: 'ES6',
            module: 'es6'
        }))
        .on('error', console.error)
        .pipe(jsmin())
        .pipe(rename({suffix: '.min'}))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(COMPONENTS_SCRIPTS_OUTPUT_DIR));
}

var scripts = gulp.parallel(buildScripts, buildMinifiedScripts);

var build = gulp.parallel(styles, scripts);

function watchAssets() {
    return gulp.watch([COMPONENTS_STYLES_REGEX, COMPONENTS_SCRIPTS_REGEX], build);
}

exports.styles = styles;
exports.scripts = scripts;
exports.build = build;
exports.dev = gulp.series(build, watchAssets);
exports.default = build;