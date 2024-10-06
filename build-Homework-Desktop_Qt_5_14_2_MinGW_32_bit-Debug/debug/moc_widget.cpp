/****************************************************************************
** Meta object code from reading C++ file 'widget.h'
**
** Created by: The Qt Meta Object Compiler version 67 (Qt 5.14.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include <memory>
#include "../../../../../Homework/widget.h"
#include <QtCore/qbytearray.h>
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'widget.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 67
#error "This file was generated using the moc from 5.14.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
QT_WARNING_PUSH
QT_WARNING_DISABLE_DEPRECATED
struct qt_meta_stringdata_Widget_t {
    QByteArrayData data[14];
    char stringdata0[185];
};
#define QT_MOC_LITERAL(idx, ofs, len) \
    Q_STATIC_BYTE_ARRAY_DATA_HEADER_INITIALIZER_WITH_OFFSET(len, \
    qptrdiff(offsetof(qt_meta_stringdata_Widget_t, stringdata0) + ofs \
        - idx * sizeof(QByteArrayData)) \
    )
static const qt_meta_stringdata_Widget_t qt_meta_stringdata_Widget = {
    {
QT_MOC_LITERAL(0, 0, 6), // "Widget"
QT_MOC_LITERAL(1, 7, 12), // "on_X_clicked"
QT_MOC_LITERAL(2, 20, 0), // ""
QT_MOC_LITERAL(3, 21, 13), // "on_XF_clicked"
QT_MOC_LITERAL(4, 35, 13), // "on_D1_clicked"
QT_MOC_LITERAL(5, 49, 13), // "on_D3_clicked"
QT_MOC_LITERAL(6, 63, 13), // "on_D7_clicked"
QT_MOC_LITERAL(7, 77, 13), // "on_D5_clicked"
QT_MOC_LITERAL(8, 91, 13), // "on_S3_clicked"
QT_MOC_LITERAL(9, 105, 13), // "on_SI_clicked"
QT_MOC_LITERAL(10, 119, 14), // "on_SII_clicked"
QT_MOC_LITERAL(11, 134, 13), // "on_S4_clicked"
QT_MOC_LITERAL(12, 148, 17), // "on_canbtn_clicked"
QT_MOC_LITERAL(13, 166, 18) // "on_initbtn_clicked"

    },
    "Widget\0on_X_clicked\0\0on_XF_clicked\0"
    "on_D1_clicked\0on_D3_clicked\0on_D7_clicked\0"
    "on_D5_clicked\0on_S3_clicked\0on_SI_clicked\0"
    "on_SII_clicked\0on_S4_clicked\0"
    "on_canbtn_clicked\0on_initbtn_clicked"
};
#undef QT_MOC_LITERAL

static const uint qt_meta_data_Widget[] = {

 // content:
       8,       // revision
       0,       // classname
       0,    0, // classinfo
      12,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: name, argc, parameters, tag, flags
       1,    0,   74,    2, 0x08 /* Private */,
       3,    0,   75,    2, 0x08 /* Private */,
       4,    0,   76,    2, 0x08 /* Private */,
       5,    0,   77,    2, 0x08 /* Private */,
       6,    0,   78,    2, 0x08 /* Private */,
       7,    0,   79,    2, 0x08 /* Private */,
       8,    0,   80,    2, 0x08 /* Private */,
       9,    0,   81,    2, 0x08 /* Private */,
      10,    0,   82,    2, 0x08 /* Private */,
      11,    0,   83,    2, 0x08 /* Private */,
      12,    0,   84,    2, 0x08 /* Private */,
      13,    0,   85,    2, 0x08 /* Private */,

 // slots: parameters
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,

       0        // eod
};

void Widget::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        auto *_t = static_cast<Widget *>(_o);
        Q_UNUSED(_t)
        switch (_id) {
        case 0: _t->on_X_clicked(); break;
        case 1: _t->on_XF_clicked(); break;
        case 2: _t->on_D1_clicked(); break;
        case 3: _t->on_D3_clicked(); break;
        case 4: _t->on_D7_clicked(); break;
        case 5: _t->on_D5_clicked(); break;
        case 6: _t->on_S3_clicked(); break;
        case 7: _t->on_SI_clicked(); break;
        case 8: _t->on_SII_clicked(); break;
        case 9: _t->on_S4_clicked(); break;
        case 10: _t->on_canbtn_clicked(); break;
        case 11: _t->on_initbtn_clicked(); break;
        default: ;
        }
    }
    Q_UNUSED(_a);
}

QT_INIT_METAOBJECT const QMetaObject Widget::staticMetaObject = { {
    QMetaObject::SuperData::link<QWidget::staticMetaObject>(),
    qt_meta_stringdata_Widget.data,
    qt_meta_data_Widget,
    qt_static_metacall,
    nullptr,
    nullptr
} };


const QMetaObject *Widget::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->dynamicMetaObject() : &staticMetaObject;
}

void *Widget::qt_metacast(const char *_clname)
{
    if (!_clname) return nullptr;
    if (!strcmp(_clname, qt_meta_stringdata_Widget.stringdata0))
        return static_cast<void*>(this);
    return QWidget::qt_metacast(_clname);
}

int Widget::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QWidget::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 12)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 12;
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        if (_id < 12)
            *reinterpret_cast<int*>(_a[0]) = -1;
        _id -= 12;
    }
    return _id;
}
QT_WARNING_POP
QT_END_MOC_NAMESPACE
