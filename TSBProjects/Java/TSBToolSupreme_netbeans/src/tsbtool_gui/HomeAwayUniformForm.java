/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package tsbtool_gui;

import java.awt.event.MouseAdapter;
import static java.awt.image.ImageObserver.WIDTH;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import javax.swing.JCheckBox;
import javax.swing.JOptionPane;
import javax.swing.event.DocumentEvent;
import javax.swing.event.DocumentListener;

/**
 *
 * @author Chris
 */
public class HomeAwayUniformForm extends javax.swing.JDialog {

    private JCheckBox[] mCheckBoxes = new JCheckBox[28];

    /**
     * Creates new form HomeAwayUniformForm
     */
    public HomeAwayUniformForm(java.awt.Frame parent, boolean modal) {
        super(parent, modal);
        initComponents();
        mCheckBoxes[0] = checkBox1;
        mCheckBoxes[1] = checkBox2;
        mCheckBoxes[2] = checkBox3;
        mCheckBoxes[3] = checkBox4;
        mCheckBoxes[4] = checkBox5;
        mCheckBoxes[5] = checkBox6;
        mCheckBoxes[6] = checkBox7;
        mCheckBoxes[7] = checkBox8;
        mCheckBoxes[8] = checkBox9;
        mCheckBoxes[9] = checkBox10;
        mCheckBoxes[10] = checkBox11;
        mCheckBoxes[11] = checkBox12;
        mCheckBoxes[12] = checkBox13;
        mCheckBoxes[13] = checkBox14;
        mCheckBoxes[14] = checkBox15;
        mCheckBoxes[15] = checkBox16;
        mCheckBoxes[16] = checkBox17;
        mCheckBoxes[17] = checkBox18;
        mCheckBoxes[18] = checkBox19;
        mCheckBoxes[19] = checkBox20;
        mCheckBoxes[20] = checkBox21;
        mCheckBoxes[21] = checkBox22;
        mCheckBoxes[22] = checkBox23;
        mCheckBoxes[23] = checkBox24;
        mCheckBoxes[24] = checkBox25;
        mCheckBoxes[25] = checkBox26;
        mCheckBoxes[26] = checkBox27;
        mCheckBoxes[27] = checkBox28;

        MouseAdapter ma = new java.awt.event.MouseAdapter() {
            @Override
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                UpdateResult();
            }
        };
        for (int i = 0; i < mCheckBoxes.length; i++) {
            mCheckBoxes[i].addMouseListener(ma);
        }
        jLabel1.setComponentPopupMenu(popupMenu);
        UpdateResult();
    }
    private Pattern mStringValueRegex = Pattern.compile("([0-9a-fA-F]{1,8})");
//    private String mStringValue;

    public String getStringValue() {
        return mResultTextBox.getText();
    }

    public void setStringValue(String value) {
        Matcher m = mStringValueRegex.matcher(value);
        if (m.find()) {
            String strVal = m.group(1);
            mResultTextBox.setText(strVal);
            long stuff = 0x00000000;
            boolean error = false;
            try {
                stuff = Long.parseLong(mResultTextBox.getText(), 16);
            } catch (Exception e) {
                error = true;
                JOptionPane.showMessageDialog(this,
                        String.format("Error setting up form, bad data '%s'", value),
                        "Error!", JOptionPane.ERROR_MESSAGE);
            }
            if (!error) {
                SetupCheckBoxes(stuff);
                if (!updating) {
                    UpdateResult();
                }
            }
        }
    }

    private void SetAllBoxes(boolean val) {
        for (int i = 0; i < mCheckBoxes.length; i++) {
            mCheckBoxes[i].setSelected(val);
        }
    }

    private void SetupCheckBoxes(long data) {
        SetAllBoxes(false);

        if ((data & 0x80000000) > 0) {
            mCheckBoxes[0].setSelected(true);
        }
        if ((data & 0x40000000) > 0) {
            mCheckBoxes[1].setSelected(true);
        }
        if ((data & 0x20000000) > 0) {
            mCheckBoxes[2].setSelected(true);
        }
        if ((data & 0x10000000) > 0) {
            mCheckBoxes[3].setSelected(true);
        }
        if ((data & 0x08000000) > 0) {
            mCheckBoxes[4].setSelected(true);
        }
        if ((data & 0x04000000) > 0) {
            mCheckBoxes[5].setSelected(true);
        }
        if ((data & 0x02000000) > 0) {
            mCheckBoxes[6].setSelected(true);
        }
        if ((data & 0x01000000) > 0) {
            mCheckBoxes[7].setSelected(true);
        }
        if ((data & 0x00800000) > 0) {
            mCheckBoxes[8].setSelected(true);
        }
        if ((data & 0x00400000) > 0) {
            mCheckBoxes[9].setSelected(true);
        }
        if ((data & 0x00200000) > 0) {
            mCheckBoxes[10].setSelected(true);
        }
        if ((data & 0x00100000) > 0) {
            mCheckBoxes[11].setSelected(true);
        }
        if ((data & 0x00080000) > 0) {
            mCheckBoxes[12].setSelected(true);
        }
        if ((data & 0x00040000) > 0) {
            mCheckBoxes[13].setSelected(true);
        }
        if ((data & 0x00020000) > 0) {
            mCheckBoxes[14].setSelected(true);
        }
        if ((data & 0x00010000) > 0) {
            mCheckBoxes[15].setSelected(true);
        }
        if ((data & 0x00008000) > 0) {
            mCheckBoxes[16].setSelected(true);
        }
        if ((data & 0x00004000) > 0) {
            mCheckBoxes[17].setSelected(true);
        }
        if ((data & 0x00002000) > 0) {
            mCheckBoxes[18].setSelected(true);
        }
        if ((data & 0x00001000) > 0) {
            mCheckBoxes[19].setSelected(true);
        }
        if ((data & 0x00000800) > 0) {
            mCheckBoxes[20].setSelected(true);
        }
        if ((data & 0x00000400) > 0) {
            mCheckBoxes[21].setSelected(true);
        }
        if ((data & 0x00000200) > 0) {
            mCheckBoxes[22].setSelected(true);
        }
        if ((data & 0x00000100) > 0) {
            mCheckBoxes[23].setSelected(true);
        }
        if ((data & 0x00000080) > 0) {
            mCheckBoxes[24].setSelected(true);
        }
        if ((data & 0x00000040) > 0) {
            mCheckBoxes[25].setSelected(true);
        }
        if ((data & 0x00000020) > 0) {
            mCheckBoxes[26].setSelected(true);
        }
        if ((data & 0x00000010) > 0) {
            mCheckBoxes[27].setSelected(true);
        }
    }

    private void UpdateResult() {
        if (!updating) {
            long tVal = getValue();
            String sVal = String.format("%08X", tVal);
            mResultTextBox.setText(sVal);
        }
    }

    public long getValue() {
        long result = 0x00000000L;
        if (mCheckBoxes[0].isSelected()) {
            result += 0x80000000L;
        }
        if (mCheckBoxes[1].isSelected()) {
            result += 0x40000000L;
        }
        if (mCheckBoxes[2].isSelected()) {
            result += 0x20000000L;
        }
        if (mCheckBoxes[3].isSelected()) {
            result += 0x10000000L;
        }
        if (mCheckBoxes[4].isSelected()) {
            result += 0x08000000L;
        }
        if (mCheckBoxes[5].isSelected()) {
            result += 0x04000000L;
        }
        if (mCheckBoxes[6].isSelected()) {
            result += 0x02000000L;
        }
        if (mCheckBoxes[7].isSelected()) {
            result += 0x01000000L;
        }
        if (mCheckBoxes[8].isSelected()) {
            result += 0x00800000L;
        }
        if (mCheckBoxes[9].isSelected()) {
            result += 0x00400000L;
        }
        if (mCheckBoxes[10].isSelected()) {
            result += 0x00200000L;
        }
        if (mCheckBoxes[11].isSelected()) {
            result += 0x00100000L;
        }
        if (mCheckBoxes[12].isSelected()) {
            result += 0x00080000L;
        }
        if (mCheckBoxes[13].isSelected()) {
            result += 0x00040000L;
        }
        if (mCheckBoxes[14].isSelected()) {
            result += 0x00020000L;
        }
        if (mCheckBoxes[15].isSelected()) {
            result += 0x00010000L;
        }
        if (mCheckBoxes[16].isSelected()) {
            result += 0x00008000L;
        }
        if (mCheckBoxes[17].isSelected()) {
            result += 0x00004000L;
        }
        if (mCheckBoxes[18].isSelected()) {
            result += 0x00002000L;
        }
        if (mCheckBoxes[19].isSelected()) {
            result += 0x00001000L;
        }
        if (mCheckBoxes[20].isSelected()) {
            result += 0x00000800L;
        }
        if (mCheckBoxes[21].isSelected()) {
            result += 0x00000400L;
        }
        if (mCheckBoxes[22].isSelected()) {
            result += 0x00000200L;
        }
        if (mCheckBoxes[23].isSelected()) {
            result += 0x00000100L;
        }
        if (mCheckBoxes[24].isSelected()) {
            result += 0x00000080L;
        }
        if (mCheckBoxes[25].isSelected()) {
            result += 0x00000040L;
        }
        if (mCheckBoxes[26].isSelected()) {
            result += 0x00000020L;
        }
        if (mCheckBoxes[27].isSelected()) {
            result += 0x00000010L;
        }

        return result;
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents()
    {

        popupMenu = new javax.swing.JPopupMenu();
        checkAllItem = new javax.swing.JMenuItem();
        uncheckAll = new javax.swing.JMenuItem();
        checkBox1 = new javax.swing.JCheckBox();
        mResultTextBox = new javax.swing.JTextField();
        mCancelButton = new javax.swing.JButton();
        mOKButton = new javax.swing.JButton();
        checkBox2 = new javax.swing.JCheckBox();
        checkBox3 = new javax.swing.JCheckBox();
        checkBox4 = new javax.swing.JCheckBox();
        checkBox5 = new javax.swing.JCheckBox();
        checkBox6 = new javax.swing.JCheckBox();
        checkBox7 = new javax.swing.JCheckBox();
        checkBox8 = new javax.swing.JCheckBox();
        checkBox9 = new javax.swing.JCheckBox();
        checkBox10 = new javax.swing.JCheckBox();
        checkBox11 = new javax.swing.JCheckBox();
        checkBox12 = new javax.swing.JCheckBox();
        checkBox13 = new javax.swing.JCheckBox();
        checkBox14 = new javax.swing.JCheckBox();
        checkBox15 = new javax.swing.JCheckBox();
        checkBox16 = new javax.swing.JCheckBox();
        checkBox17 = new javax.swing.JCheckBox();
        checkBox18 = new javax.swing.JCheckBox();
        checkBox19 = new javax.swing.JCheckBox();
        checkBox20 = new javax.swing.JCheckBox();
        checkBox21 = new javax.swing.JCheckBox();
        checkBox22 = new javax.swing.JCheckBox();
        checkBox23 = new javax.swing.JCheckBox();
        checkBox24 = new javax.swing.JCheckBox();
        checkBox25 = new javax.swing.JCheckBox();
        checkBox26 = new javax.swing.JCheckBox();
        checkBox27 = new javax.swing.JCheckBox();
        checkBox28 = new javax.swing.JCheckBox();
        jLabel1 = new javax.swing.JLabel();

        checkAllItem.setText("Check All");
        checkAllItem.addActionListener(new java.awt.event.ActionListener()
        {
            public void actionPerformed(java.awt.event.ActionEvent evt)
            {
                checkAllItemActionPerformed(evt);
            }
        });
        popupMenu.add(checkAllItem);

        uncheckAll.setText("Uncheck All");
        uncheckAll.addActionListener(new java.awt.event.ActionListener()
        {
            public void actionPerformed(java.awt.event.ActionEvent evt)
            {
                uncheckAllActionPerformed(evt);
            }
        });
        popupMenu.add(uncheckAll);

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setTitle("Use Uniform 2 Against");
        setBackground(new java.awt.Color(0, 113, 239));
        setIconImage(MainGUI.GetImage("/tsbtool_gui/icons/49ers.png", this));
        setMaximumSize(new java.awt.Dimension(495, 423));
        setMinimumSize(new java.awt.Dimension(495, 423));
        setPreferredSize(new java.awt.Dimension(495, 423));
        getContentPane().setLayout(null);

        checkBox1.setBackground(new java.awt.Color(0, 113, 239));
        checkBox1.setText(" ");
        checkBox1.setOpaque(true);
        checkBox1.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox1);
        checkBox1.setBounds(10, 10, 20, 20);
        getContentPane().add(mResultTextBox);
        mResultTextBox.setBounds(0, 350, 220, 28);

        mCancelButton.setText("Cancel");
        mCancelButton.addActionListener(new java.awt.event.ActionListener()
        {
            public void actionPerformed(java.awt.event.ActionEvent evt)
            {
                mCancelButtonActionPerformed(evt);
            }
        });
        getContentPane().add(mCancelButton);
        mCancelButton.setBounds(390, 350, 80, 28);

        mOKButton.setText("OK");
        mOKButton.addActionListener(new java.awt.event.ActionListener()
        {
            public void actionPerformed(java.awt.event.ActionEvent evt)
            {
                mOKButtonActionPerformed(evt);
            }
        });
        getContentPane().add(mOKButton);
        mOKButton.setBounds(300, 350, 80, 28);

        checkBox2.setBackground(new java.awt.Color(0, 113, 239));
        checkBox2.setText(" ");
        checkBox2.setOpaque(true);
        checkBox2.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox2);
        checkBox2.setBounds(10, 50, 20, 20);

        checkBox3.setBackground(new java.awt.Color(0, 113, 239));
        checkBox3.setText(" ");
        checkBox3.setOpaque(true);
        checkBox3.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox3);
        checkBox3.setBounds(10, 80, 20, 20);

        checkBox4.setBackground(new java.awt.Color(0, 113, 239));
        checkBox4.setText(" ");
        checkBox4.setOpaque(true);
        checkBox4.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox4);
        checkBox4.setBounds(10, 110, 20, 20);

        checkBox5.setBackground(new java.awt.Color(0, 113, 239));
        checkBox5.setText(" ");
        checkBox5.setOpaque(true);
        checkBox5.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox5);
        checkBox5.setBounds(10, 140, 20, 20);

        checkBox6.setBackground(new java.awt.Color(0, 113, 239));
        checkBox6.setText(" ");
        checkBox6.setOpaque(true);
        checkBox6.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox6);
        checkBox6.setBounds(170, 10, 20, 20);

        checkBox7.setBackground(new java.awt.Color(0, 113, 239));
        checkBox7.setText(" ");
        checkBox7.setOpaque(true);
        checkBox7.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox7);
        checkBox7.setBounds(170, 40, 20, 20);

        checkBox8.setBackground(new java.awt.Color(0, 113, 239));
        checkBox8.setText(" ");
        checkBox8.setOpaque(true);
        checkBox8.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox8);
        checkBox8.setBounds(170, 70, 20, 20);

        checkBox9.setBackground(new java.awt.Color(0, 113, 239));
        checkBox9.setText(" ");
        checkBox9.setOpaque(true);
        checkBox9.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox9);
        checkBox9.setBounds(170, 100, 20, 20);

        checkBox10.setBackground(new java.awt.Color(0, 113, 239));
        checkBox10.setText(" ");
        checkBox10.setOpaque(true);
        checkBox10.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox10);
        checkBox10.setBounds(320, 20, 20, 20);

        checkBox11.setBackground(new java.awt.Color(0, 113, 239));
        checkBox11.setText(" ");
        checkBox11.setOpaque(true);
        checkBox11.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox11);
        checkBox11.setBounds(320, 50, 20, 20);

        checkBox12.setBackground(new java.awt.Color(0, 113, 239));
        checkBox12.setText(" ");
        checkBox12.setOpaque(true);
        checkBox12.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox12);
        checkBox12.setBounds(320, 80, 20, 20);

        checkBox13.setBackground(new java.awt.Color(0, 113, 239));
        checkBox13.setText(" ");
        checkBox13.setOpaque(true);
        checkBox13.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox13);
        checkBox13.setBounds(320, 110, 20, 20);

        checkBox14.setBackground(new java.awt.Color(0, 113, 239));
        checkBox14.setText(" ");
        checkBox14.setOpaque(true);
        checkBox14.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox14);
        checkBox14.setBounds(320, 140, 20, 20);

        checkBox15.setBackground(new java.awt.Color(0, 113, 239));
        checkBox15.setText(" ");
        checkBox15.setOpaque(true);
        checkBox15.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox15);
        checkBox15.setBounds(10, 170, 20, 20);

        checkBox16.setBackground(new java.awt.Color(0, 113, 239));
        checkBox16.setText(" ");
        checkBox16.setOpaque(true);
        checkBox16.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox16);
        checkBox16.setBounds(10, 200, 20, 20);

        checkBox17.setBackground(new java.awt.Color(0, 113, 239));
        checkBox17.setText(" ");
        checkBox17.setOpaque(true);
        checkBox17.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox17);
        checkBox17.setBounds(10, 230, 20, 20);

        checkBox18.setBackground(new java.awt.Color(0, 113, 239));
        checkBox18.setText(" ");
        checkBox18.setOpaque(true);
        checkBox18.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox18);
        checkBox18.setBounds(10, 260, 20, 20);

        checkBox19.setBackground(new java.awt.Color(0, 113, 239));
        checkBox19.setText(" ");
        checkBox19.setOpaque(true);
        checkBox19.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox19);
        checkBox19.setBounds(10, 290, 20, 20);

        checkBox20.setBackground(new java.awt.Color(0, 113, 239));
        checkBox20.setText(" ");
        checkBox20.setOpaque(true);
        checkBox20.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox20);
        checkBox20.setBounds(170, 170, 20, 20);

        checkBox21.setBackground(new java.awt.Color(0, 113, 239));
        checkBox21.setText(" ");
        checkBox21.setOpaque(true);
        checkBox21.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox21);
        checkBox21.setBounds(170, 200, 20, 20);

        checkBox22.setBackground(new java.awt.Color(0, 113, 239));
        checkBox22.setText(" ");
        checkBox22.setOpaque(true);
        checkBox22.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox22);
        checkBox22.setBounds(170, 240, 20, 20);

        checkBox23.setBackground(new java.awt.Color(0, 113, 239));
        checkBox23.setText(" ");
        checkBox23.setOpaque(true);
        checkBox23.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox23);
        checkBox23.setBounds(170, 270, 20, 20);

        checkBox24.setBackground(new java.awt.Color(0, 113, 239));
        checkBox24.setText(" ");
        checkBox24.setOpaque(true);
        checkBox24.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox24);
        checkBox24.setBounds(170, 300, 20, 20);

        checkBox25.setBackground(new java.awt.Color(0, 113, 239));
        checkBox25.setText(" ");
        checkBox25.setOpaque(true);
        getContentPane().add(checkBox25);
        checkBox25.setBounds(320, 210, 20, 20);

        checkBox26.setBackground(new java.awt.Color(0, 113, 239));
        checkBox26.setText(" ");
        checkBox26.setOpaque(true);
        checkBox26.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox26);
        checkBox26.setBounds(320, 240, 20, 20);

        checkBox27.setBackground(new java.awt.Color(0, 113, 239));
        checkBox27.setText(" ");
        checkBox27.setOpaque(true);
        checkBox27.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox27);
        checkBox27.setBounds(320, 270, 20, 20);

        checkBox28.setBackground(new java.awt.Color(0, 113, 239));
        checkBox28.setText(" ");
        checkBox28.setOpaque(true);
        checkBox28.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                checkBoxClicked(evt);
            }
        });
        getContentPane().add(checkBox28);
        checkBox28.setBounds(320, 300, 20, 20);

        jLabel1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/tsbtool_gui/icons/TeamSelect.PNG"))); // NOI18N
        getContentPane().add(jLabel1);
        jLabel1.setBounds(0, 0, 480, 340);

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void checkBoxClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_checkBoxClicked
        UpdateResult();
    }//GEN-LAST:event_checkBoxClicked

    private void mOKButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mOKButtonActionPerformed
        mCanceled = false;
        setVisible(false);
    }//GEN-LAST:event_mOKButtonActionPerformed

    private void mCancelButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mCancelButtonActionPerformed
        mCanceled = true;
        setVisible(false);
    }//GEN-LAST:event_mCancelButtonActionPerformed

    private void checkAllItemActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_checkAllItemActionPerformed
        SetAllBoxes(true);
        UpdateResult();
    }//GEN-LAST:event_checkAllItemActionPerformed

    private void uncheckAllActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_uncheckAllActionPerformed
        SetAllBoxes(false);
        UpdateResult();
    }//GEN-LAST:event_uncheckAllActionPerformed
    private boolean updating = false;
    private boolean mCanceled = false;

    /**
     *
     * @return true if the user canceled the dialog, false if confirmed.
     */
    public boolean getCanceled() {
        return mCanceled;
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(HomeAwayUniformForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(HomeAwayUniformForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(HomeAwayUniformForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(HomeAwayUniformForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the dialog */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                HomeAwayUniformForm dialog = new HomeAwayUniformForm(new javax.swing.JFrame(), true);
                dialog.addWindowListener(new java.awt.event.WindowAdapter() {
                    @Override
                    public void windowClosing(java.awt.event.WindowEvent e) {
                        System.exit(0);
                    }
                });
                dialog.setVisible(true);
            }
        });
    }
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JMenuItem checkAllItem;
    private javax.swing.JCheckBox checkBox1;
    private javax.swing.JCheckBox checkBox10;
    private javax.swing.JCheckBox checkBox11;
    private javax.swing.JCheckBox checkBox12;
    private javax.swing.JCheckBox checkBox13;
    private javax.swing.JCheckBox checkBox14;
    private javax.swing.JCheckBox checkBox15;
    private javax.swing.JCheckBox checkBox16;
    private javax.swing.JCheckBox checkBox17;
    private javax.swing.JCheckBox checkBox18;
    private javax.swing.JCheckBox checkBox19;
    private javax.swing.JCheckBox checkBox2;
    private javax.swing.JCheckBox checkBox20;
    private javax.swing.JCheckBox checkBox21;
    private javax.swing.JCheckBox checkBox22;
    private javax.swing.JCheckBox checkBox23;
    private javax.swing.JCheckBox checkBox24;
    private javax.swing.JCheckBox checkBox25;
    private javax.swing.JCheckBox checkBox26;
    private javax.swing.JCheckBox checkBox27;
    private javax.swing.JCheckBox checkBox28;
    private javax.swing.JCheckBox checkBox3;
    private javax.swing.JCheckBox checkBox4;
    private javax.swing.JCheckBox checkBox5;
    private javax.swing.JCheckBox checkBox6;
    private javax.swing.JCheckBox checkBox7;
    private javax.swing.JCheckBox checkBox8;
    private javax.swing.JCheckBox checkBox9;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JButton mCancelButton;
    private javax.swing.JButton mOKButton;
    private javax.swing.JTextField mResultTextBox;
    private javax.swing.JPopupMenu popupMenu;
    private javax.swing.JMenuItem uncheckAll;
    // End of variables declaration//GEN-END:variables
}
