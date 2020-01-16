package scxmlgen.Modalities;

import scxmlgen.interfaces.IOutput;



public enum Output implements IOutput{
    CLOSE_TAB("[0]"), //tirei para mostrar que funciona
    OPEN_HISTORIC("[1]"),
    MAXIMIZE("[2]"),
    MINIMIZE("[3]"),
    OPEN_TAB("[4]"),
    SCROLL_DOWN("[5][1]"),
    SCROLL_DOWN_2("[5][2]"),
    SCROLL_DOWN_3("[5][3]"),
    SCROLL_DOWN_TOTAL("[5][total]"),
    SCROLL_UP("[6][1]"),
    SCROLL_UP_2("[6][2]"),
    SCROLL_UP_3("[6][3]"),
    SCROLL_UP_TOTAL("[6][total]"),
    ZOOM_IN("[7][1]"),
    ZOOM_IN_2("[7][2]"),
    ZOOM_IN_3("[7][3]"),
    ZOOM_IN_TOTAL("[7][9]"),
    ZOOM_OUT("[8][1]"),
    ZOOM_OUT_2("[8][2]"),
    ZOOM_OUT_3("[8][3]"),
    ZOOM_OUT_TOTAL("[8][9]"),
    ADD_FAV("[11]"),
    OPEN_FAV("[12]"),
    NEXT_TAB("[9]"),
    PREVIOUS_TAB("[9]")
    ;
    
    
    
    private String event;

    Output(String m) {
        event=m;
    }
    
    public String getEvent(){
        return this.toString();
    }

    public String getEventName(){
        return event;
    }
}
