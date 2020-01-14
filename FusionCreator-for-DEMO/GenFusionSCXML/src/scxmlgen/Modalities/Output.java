package scxmlgen.Modalities;

import scxmlgen.interfaces.IOutput;



public enum Output implements IOutput{
    CLOSE_TAB("[0]"),
    OPEN_HISTORIC("[1]"),
    MAXIMIZE("[2]"),
    MINIMIZE("[3]"),
    OPEN_TAB("[4]"),
    SCROLL_DOWN("[5,1]"),
    SCROLL_UP("[6,2]"),
    ZOOM_IN("[7,2]"),
    ZOOM_OUT("[8,2]"),
    NEXT_TAB("[9]"),
    PREVIOUS_TAB("[10]"),
    ADD_FAV("[11]"),
    OPEN_FAV("[12]"),
    OPEN_LINK("[13]"),
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
